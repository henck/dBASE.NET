using dBASE.NET.Decoders;
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
		private List<DbfField> fields;
		private object[] data;

		public DbfRecord(BinaryReader reader, DbfHeader header, List<DbfField> fields, byte[] memoData)
		{
			this.fields = fields;
			data = new object[fields.Count];

			// Read record marker.
			byte marker = reader.ReadByte();

			// Read entire record as sequence of bytes.
			// Note that record length includes marker.
			byte[] row = reader.ReadBytes(header.RecordLength - 1); 

			// Read data for each field.
			int offset = 0;
			int index = 0;
			foreach (DbfField field in fields)
			{
				// Copy bytes from record buffer into field buffer, 
				// and convert these bytes into a string.
				byte[] buffer = new byte[field.Length];
				Array.Copy(row, offset, buffer, 0, field.Length);
				offset += field.Length;

				IDecoder decoder = DecoderFactory.GetDecoder(field.Type);
				data[index] = decoder.Decode(buffer, memoData);
				index++;
			}
		}

		public object[] Data {
			get
			{
				return data;
			}
		}

	  public object this[int index]
		{
			get
			{
				return data[index];
			}
		}

		public object this[string name]
		{
			get
			{
				int index = fields.FindIndex(x => x.Name.Equals(name));
				if (index == -1) return null;
				return data[index];
			}
		}

		public object this[DbfField field]
		{
			get
			{
				int index = fields.IndexOf(field);
				if (index == -1) return null;
				return data[index];
			}
		}
	}
}
