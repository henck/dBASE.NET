namespace dBASE.NET {
    using dBASE.NET.Encoders;
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// DbfRecord encapsulates a record in a .dbf file. It contains an array with
    /// data (as an Object) for each field.
    /// </summary>
    public class DbfRecord {
        private const string defaultSeparator = ",";
        private const string defaultMask = "{name}={value}";

        public Dbf ParentDbf;

        private List<DbfField> fields;

        internal DbfRecord(BinaryReader reader, DbfHeader header, List<DbfField> fields, byte[] memoData, Encoding encoding) {
            this.fields = fields;
            Data = new List<object>();
            
            // Read record marker.
            byte marker = reader.ReadByte();

            // Read entire record as sequence of bytes.
            // Note that record length includes marker.
            byte[] row = reader.ReadBytes(header.RecordLength - 1);
            if (row.Length == 0)
                throw new EndOfStreamException();

            // Read data for each field.
            int offset = 0;
            foreach (DbfField field in fields) {
                // Copy bytes from record buffer into field buffer.
                byte[] buffer = new byte[field.Length];
                Array.Copy(row, offset, buffer, 0, field.Length);
                offset += field.Length;

                IEncoder encoder = EncoderFactory.GetEncoder(field.Type);
                Data.Add(encoder.Decode(buffer, memoData, encoding));
            }
        }

        /// <summary>
        /// Create an empty record.
        /// </summary>
        internal DbfRecord(List<DbfField> fields) {
            this.fields = fields;
            Data = new List<object>();
            foreach (DbfField field in fields) Data.Add(null);
        }

        public List<object> Data { get; }

        public object this[int index] => Data[index];

        public object this[string name] {
            get {
                int index = fields.FindIndex(x => x.Name.Equals(name));
                if (index == -1) return null;
                return Data[index];
            }
        }

        public object this[DbfField field] {
            get {
                int index = fields.IndexOf(field);
                if (index == -1) return null;
                return Data[index];
            }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() {
            return ToString(defaultSeparator, defaultMask);
        }

        /// <summary>
        /// Returns a string that represents the current object with custom separator.
        /// </summary>
        /// <param name="separator">Custom separator.</param>
        /// <returns>A string that represents the current object with custom separator.</returns>
        public string ToString(string separator) {
            return ToString(separator, defaultMask);
        }

        /// <summary>
        /// Returns a string that represents the current object with custom separator and mask.
        /// </summary>
        /// <param name="separator">Custom separator.</param>
        /// <param name="mask">
        /// Custom mask.
        /// <para>e.g., "{name}={value}", where {name} is the mask for the field name, and {value} is the mask for the value.</para>
        /// </param>
        /// <returns>A string that represents the current object with custom separator and mask.</returns>
        public string ToString(string separator, string mask) {
            separator = separator ?? defaultSeparator;
            mask = (mask ?? defaultMask).Replace("{name}", "{0}").Replace("{value}", "{1}");

            return string.Join(separator, fields.Select(z => string.Format(mask, z.Name, this[z])));
        }

        internal void Write(BinaryWriter writer, Encoding encoding) {
            // Emanuele Bonin 22/03/2025
            // VFP MemoFile
            string MemoFile;

            MemoFile = Path.ChangeExtension(ParentDbf.DBFPath, "fpt");

            // Write marker (always "not deleted")
            writer.Write((byte)0x20);

            int index = 0;

            FileStream stream = File.Open(MemoFile, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryWriter Memowriter = new BinaryWriter(stream, Encoding.ASCII);
            BinaryReader Memoreader = new BinaryReader(stream, Encoding.ASCII);

            int BlockSize, FreeBlockPointer;

            // Read 32-bit integer as big endian
            byte[] bytes = Memoreader.ReadBytes(4); // 0x00 - 0x03 next free block
            Array.Reverse(bytes);
            FreeBlockPointer = BitConverter.ToInt32(bytes, 0);
            Memoreader.BaseStream.Seek(6, SeekOrigin.Begin); // 0x06 - 0x07 block size in bytes
            bytes = Memoreader.ReadBytes(2);
            Array.Reverse(bytes);
            BlockSize = BitConverter.ToInt16(bytes, 0);
            // manca la scrttura dei dati e il ritorno del puntatore
            // aggiornare anche il puntatore al primo blocco libero
            // https://www.vfphelp.com/help/_5WN12PC0N.htm
            foreach (DbfField field in fields) {
                IEncoder encoder = EncoderFactory.GetEncoder(field.Type);
                byte[] buffer = encoder.Encode(field, Data[index], encoding);
                if (field.Type == DbfFieldType.Memo) {
                    // Write on memo file
                    // and get the memo position
                    Memowriter.Seek(FreeBlockPointer * BlockSize, SeekOrigin.Begin);
                    Memowriter.Write(BitConverter.GetBytes((int)1).Reverse().ToArray()); // 0x00 - 0x03 Block signature Big Endian
                                                                                         // (indicates the type of data in the block)
                                                                                         // 0 – picture (picture field type)
                                                                                         // 1 – text (memo field type)

                    Memowriter.Write(BitConverter.GetBytes((int)buffer.Length).Reverse().ToArray()); // Length of memo(in bytes) Big Endian
                    Memowriter.Write(buffer);
                    int UsedBlocks = (int)Math.Ceiling(buffer.Length / (decimal)BlockSize);
                    // Fill rest of the used blocks with 0x00
                    for (int i = 0; i < UsedBlocks * BlockSize - buffer.Length; i++) {
                        Memowriter.Write((byte)0x00);
                    }


                    buffer = BitConverter.GetBytes((int)FreeBlockPointer);
                    FreeBlockPointer = FreeBlockPointer + UsedBlocks;
                }
                if (buffer.Length > field.Length)
                    throw new ArgumentOutOfRangeException(nameof(buffer.Length), buffer.Length, "Buffer length has exceeded length of the field.");
                writer.Write(buffer);
                index++;
            }
            Memowriter.Seek(0, SeekOrigin.Begin);
            Memowriter.Write(BitConverter.GetBytes((int)FreeBlockPointer * BlockSize).Reverse().ToArray());
            Memowriter.Close();
            Memoreader.Close();

        }
    }
}
