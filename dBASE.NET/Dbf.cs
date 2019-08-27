namespace dBASE.NET
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// The Dbf class encapsulated a dBASE table (.dbf) file, allowing
    /// reading from disk, writing to disk, enumerating fields and enumerating records.
    /// </summary>
    public class Dbf
    {
        private DbfHeader header;

        public Dbf()
        {
            this.header = DbfHeader.CreateHeader(DbfVersion.FoxBaseDBase3NoMemo);
            this.Fields = new List<DbfField>();
            this.Records = new List<DbfRecord>();
        }

        public Dbf(Encoding encoding)
            : this()
        {
            Encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
        }

        public List<DbfField> Fields { get; }

        public List<DbfRecord> Records { get; }

        public Encoding Encoding { get; } = Encoding.ASCII;

        public DbfRecord CreateRecord()
        {
            DbfRecord record = new DbfRecord(Fields);
            this.Records.Add(record);
            return record;
        }

        public void Read(String path)
        {
            // Open stream for reading.
            FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);

            ReadHeader(reader);
            byte[] memoData = ReadMemos(path);
            ReadFields(reader);

            // After reading the fields, we move the read pointer to the beginning
            // of the records, as indicated by the "HeaderLength" value in the header.
            stream.Seek(header.HeaderLength, SeekOrigin.Begin);

            ReadRecords(reader, memoData);

            // Close stream.
            reader.Close();
            stream.Close();
        }

        private byte[] ReadMemos(string path)
        {
            String memoPath = Path.ChangeExtension(path, "fpt");
            if (!File.Exists(memoPath))
            {
                memoPath = Path.ChangeExtension(path, "dbt");
                if (!File.Exists(memoPath))
                {
                    return null;
                }
            }

            FileStream str = File.Open(memoPath, FileMode.Open, FileAccess.Read);
            BinaryReader memoReader = new BinaryReader(str);
            byte[] memoData = new byte[str.Length];
            memoData = memoReader.ReadBytes((int)str.Length);
            memoReader.Close();
            str.Close();
            return memoData;
        }

        private void ReadHeader(BinaryReader reader)
        {
            // Peek at version number, then try to read correct version header.
            byte versionByte = reader.ReadByte();
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            DbfVersion version = (DbfVersion)versionByte;
            header = DbfHeader.CreateHeader(version);
            header.Read(reader);
        }

        private void ReadFields(BinaryReader reader)
        {
            Fields.Clear();

            // Fields are terminated by 0x0d char.
            while (reader.PeekChar() != 0x0d)
            {
                Fields.Add(new DbfField(reader, Encoding));
            }

            // Read fields terminator.
            reader.ReadByte();
        }

        private void ReadRecords(BinaryReader reader, byte[] memoData)
        {
            Records.Clear();

            // Records are terminated by 0x1a char (officially), or EOF (also seen).
            while (reader.PeekChar() != 0x1a && reader.PeekChar() != -1)
            {
                Records.Add(new DbfRecord(reader, header, Fields, memoData, Encoding));
            }
        }

        public void Write(String path, DbfVersion version = DbfVersion.Unknown)
        {
            // Use version specified. If unknown specified, use current header version.
            if (version != DbfVersion.Unknown) header.Version = version;
            header = DbfHeader.CreateHeader(header.Version);

            FileStream stream = File.Open(path, FileMode.Create, FileAccess.Write);
            BinaryWriter writer = new BinaryWriter(stream);

            header.Write(writer, Fields, Records);
            WriteFields(writer);
            WriteRecords(writer);

            writer.Close();
            stream.Close();
        }

        private void WriteFields(BinaryWriter writer)
        {
            foreach (DbfField field in Fields)
            {
                field.Write(writer, Encoding);
            }

            // Write field descriptor array terminator.
            writer.Write((byte)0x0d);
        }

        private void WriteRecords(BinaryWriter writer)
        {
            foreach (DbfRecord record in Records)
            {
                record.Write(writer, Encoding);
            }

            // Write EOF character.
            writer.Write((byte)0x1a);
        }
    }
}
