namespace dBASE.NET
{
    using dBASE.NET.Encoders;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// DbfRecord encapsulates a record in a .dbf file. It contains an array with
    /// data (as an Object) for each field.
    /// </summary>
    public class DbfRecord
    {
        private List<DbfField> fields;

        internal DbfRecord(BinaryReader reader, DbfHeader header, List<DbfField> fields, byte[] memoData, Encoding encoding)
        {
            this.fields = fields;
            Data = new List<object>();

            // Read record marker.
            byte marker = reader.ReadByte();

            // Read entire record as sequence of bytes.
            // Note that record length includes marker.
            byte[] row = reader.ReadBytes(header.RecordLength - 1);

            // Read data for each field.
            int offset = 0;
            foreach (DbfField field in fields)
            {
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
        internal DbfRecord(List<DbfField> fields)
        {
            this.fields = fields;
            Data = new List<object>();
            foreach (DbfField field in fields) Data.Add(null);
        }

        public List<object> Data { get; }

        public object this[int index] => Data[index];

        public object this[string name]
        {
            get
            {
                int index = fields.FindIndex(x => x.Name.Equals(name));
                if (index == -1) return null;
                return Data[index];
            }
        }

        public object this[DbfField field]
        {
            get
            {
                int index = fields.IndexOf(field);
                if (index == -1) return null;
                return Data[index];
            }
        }

        internal void Write(BinaryWriter writer, Encoding encoding)
        {
            // Write marker (always "not deleted")
            writer.Write((byte)0x20);

            int index = 0;
            foreach (DbfField field in fields)
            {
                IEncoder encoder = EncoderFactory.GetEncoder(field.Type);
                byte[] buffer = encoder.Encode(field, Data[index], encoding);
                if (buffer.Length > field.Length)
                    throw new ArgumentOutOfRangeException(nameof(buffer.Length), buffer.Length, "Buffer length has exceeded length of the field.");

                writer.Write(buffer);
                index++;
            }
        }
    }
}
