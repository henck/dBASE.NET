using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dBASE.NET
{
	/// <summary>
	/// DbfRecord encapsulates a record in a .dbf file. It conains a dictionary with
	/// data (as an Object) for each field.
	/// </summary>
	public class DbfRecord
	{
		private Dictionary<DbfField, object> data;

		public DbfRecord(BinaryReader reader, DbfHeader header, List<DbfField> fields)
		{
			data = new Dictionary<DbfField, object>();

			// Read record marker.
			byte marker = reader.ReadByte();

			// Read entire record as sequence of bytes.
			// Note that record length includes marker.
			byte[] row = reader.ReadBytes(header.RecordLength - 1); 

			// Read data for each field.
			int offset = 0;
			foreach (DbfField field in fields)
			{
				// Copy bytes from record buffer into field buffer, 
				// and convert these bytes into a string.
				byte[] buffer = new byte[field.Length];
				Array.Copy(row, offset, buffer, 0, field.Length);
				offset += field.Length;
				string text = Encoding.ASCII.GetString(buffer).Trim();

				// If buffer is empty, when we have a NULL-value.
				if(text.Length == 0)
				{
					data[field] = null;
					continue;
				}

				switch (field.Type)
				{
					case DbfFieldType.Character:
						data[field] = text;
						break;
					case DbfFieldType.Date:
						data[field] = DateTime.ParseExact(text, "yyyyMMdd", CultureInfo.InvariantCulture);
						break;
					case DbfFieldType.Numeric:
						data[field] = Convert.ToSingle(text);
						break;
					case DbfFieldType.Float:
						data[field] = Convert.ToSingle(text);
						break;
					case DbfFieldType.Logical:
						data[field] = (buffer[0] == 'Y' || buffer[0] == 'y' || buffer[0] == 'T' || buffer[0] == 't');
						break;
					default:
						throw new Exception("Unsupported field type.");
				}
			}
		}

		public Dictionary<DbfField, object> Data {
			get
			{
				return data;
			}
		}
	}
}
