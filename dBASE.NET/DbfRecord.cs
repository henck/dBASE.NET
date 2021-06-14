namespace dBASE.NET
{
    using dBASE.NET.Encoders;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// DbfRecord encapsulates a record in a .dbf file. It contains an array with
    /// data (as an Object) for each field.
    /// </summary>
    public class DbfRecord
    {
        private const string defaultSeparator = ",";
        private const string defaultMask = "{name}={value}";

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
            if (row.Length == 0)
                throw new EndOfStreamException();

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
                int index = GetFieldIndex(name);
                if (index == -1) return null;
                return Data[index];
            }
        }

        public object this[DbfField field]
        {
            get
            {
                int index = GetFieldIndex(field);
                if (index == -1) return null;
                return Data[index];
            }
        }

        /// <summary>
        /// Return the index of the field by it's name.
        /// </summary>
        /// <param name="fieldName">Name of the field</param>
        /// <returns></returns>
        public int GetFieldIndex(string fieldName)
        {
            return fields.FindIndex(x => x.Name.Equals(fieldName));
        }

        /// <summary>
        /// Return the index of a field
        /// </summary>
        /// <param name="field">The field</param>
        /// <returns></returns>
        public int GetFieldIndex(DbfField field)
        {
            return fields.IndexOf(field);
        }

        /// <summary>
        /// Return the list of fields in the current record.
        /// </summary>
        public List<DbfField> Fields
        {
            get
            {
                return fields;
            }
        }

        public void FromEntity<T>(T obj)
        {
            var properties = GetDecoratedProperties(obj);

            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute(typeof(DbfFieldAttribute)) as DbfFieldAttribute;

                if (attribute == null)
                {
                    throw new InvalidOperationException(
                        $"Property {property.Name} does not have the DbfField attribute!"
                    );
                }

                if (property.CanRead)
                {
                    Data[GetFieldIndex(attribute.Name)] = property.GetValue(obj);
                }
            }
        }

        public void ToEntity<T>(T obj)
        {
            var properties = GetDecoratedProperties(obj);

            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute(typeof(DbfFieldAttribute)) as DbfFieldAttribute;

                if(attribute == null)
                {
                    throw new InvalidOperationException(
                        $"Property {property.Name} does not have the DbfField attribute!"
                    );
                }

                if(property.CanWrite)
                {
                    property.SetValue(obj, Data[GetFieldIndex(attribute.Name)]);
                }
            }
        }

        internal PropertyInfo[] GetDecoratedProperties(object obj)
        {
            var decoratedProperties = new List<PropertyInfo>();

            var allProperties = obj.GetType().GetProperties();

            foreach (var property in allProperties)
            {
                var attributes = property.GetCustomAttributes();
                foreach (var attr in attributes)
                {
                    if(attr is DbfFieldAttribute)
                    {
                        decoratedProperties.Add(property);
                    }
                }
            }

            return decoratedProperties.ToArray();
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ToString(defaultSeparator, defaultMask);
        }

        /// <summary>
        /// Returns a string that represents the current object with custom separator.
        /// </summary>
        /// <param name="separator">Custom separator.</param>
        /// <returns>A string that represents the current object with custom separator.</returns>
        public string ToString(string separator)
        {
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
        public string ToString(string separator, string mask)
        {
            separator = separator ?? defaultSeparator;
            mask = (mask ?? defaultMask).Replace("{name}", "{0}").Replace("{value}", "{1}");

            return string.Join(separator, fields.Select(z => string.Format(mask, z.Name, this[z])));
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
