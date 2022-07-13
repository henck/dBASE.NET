namespace dBASE.NET
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Encapsulates a field descriptor in a .dbf file.
    /// </summary>
    public class DbfField : IEquatable<DbfField>
    {
        private string defaultValue;

        /// <summary>
        /// Field name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Field type
        /// </summary>
        public DbfFieldType Type { get; set; }

        /// <summary>
        /// Length of field in characters/bytes
        /// </summary>
        public byte Length { get; set; }

#pragma warning disable 1591
        public byte Precision { get; set; }

        public byte WorkAreaID { get; set; }

        public byte Flags { get; set; }
#pragma warning restore 1591
        
        /// <summary>
        /// Default value to write.
        /// </summary>
        internal string DefaultValue => defaultValue ??= new string(' ', Length);
#pragma warning disable 1591
        public DbfField(string name, DbfFieldType type, byte length, byte precision = 0)
        {
            Name = name;
            Type = type;
            Length = length;
            Precision = precision;
            WorkAreaID = 0;
            Flags = 0;
        }
#pragma warning restore 1591
        /// <inheritdoc />
        public bool Equals(DbfField other)
        {
            return other != null
                   && Name == other.Name
                   && Type == other.Type
                   && Length == other.Length
                   && Precision == other.Precision
                   && WorkAreaID == other.WorkAreaID
                   && Flags == other.Flags;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is DbfField other && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, (int) Type, Length, Precision, WorkAreaID, Flags);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"Name = `{Name}`, Type = `{(char)Type}`, Length = `{Length}`, Precision = `{Precision}`";
        }

        internal DbfField(BinaryReader reader, Encoding encoding)
        {
            // Some field name maybe like `NUM\0\0?B\0\0\0\0`, so we should split by `\0` instead of end trimming.
            var rawName = encoding.GetString(reader.ReadBytes(11));
            Name = rawName.Split((char)0)[0];
            Type = (DbfFieldType)reader.ReadByte();
            reader.ReadBytes(4); // reserved: Field data address in memory.
            Length = reader.ReadByte();
            Precision = reader.ReadByte();
            reader.ReadBytes(2); // reserved.
            WorkAreaID = reader.ReadByte();
            reader.ReadBytes(2); // reserved.
            Flags = reader.ReadByte();
            reader.ReadBytes(8);
        }

        internal void Write(BinaryWriter writer, Encoding encoding)
        {
            // Pad field name with 0-bytes, then save it.
            string name = this.Name;
            if (name.Length > 11) name = name.Substring(0, 11);
            while (name.Length < 11) name += '\0';
            byte[] nameBytes = encoding.GetBytes(name);
            writer.Write(nameBytes); // 11 bytes.

            writer.Write((char)Type);
            writer.Write((uint)0); // 4 reserved bytes: Field data address in memory.
            writer.Write(Length);
            writer.Write(Precision);
            writer.Write((ushort)0); // 2 reserved byte.
            writer.Write(WorkAreaID);
            writer.Write((ushort)0); // 2 reserved byte.
            writer.Write(Flags);

            for (var i = 0; i < 8; i++) writer.Write((byte)0); // 8 reserved bytes.
        }
    }
}
