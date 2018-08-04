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
		private Dictionary<DbfField, object> data;

		public DbfRecord(BinaryReader reader, DbfHeader header, List<DbfField> fields, byte[] memoData)
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

				IDecoder decoder = DecoderFactory.GetDecoder(field.Type);
				data[field] = decoder.Decode(buffer, memoData);
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
