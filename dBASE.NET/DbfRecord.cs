using dBASE.NET.Decoders;
using dBASE.NET.Encoders;
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
	/// DbfRecord encapsulates a record in a .dbf file. It contains an array with
	/// data (as an Object) for each field.
	/// </summary>
	public class DbfRecord
	{
		private List<DbfField> fields;
		private List<object> data;

		internal DbfRecord(BinaryReader reader, DbfHeader header, List<DbfField> fields, byte[] memoData)
		{
			this.fields = fields;
			data = new List<object>();

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

				IDecoder decoder = DecoderFactory.GetDecoder(field.Type);
				data.Add(decoder.Decode(buffer, memoData));
			}
		}

		/// <summary>
		/// Create an empty record.
		/// </summary>
		internal DbfRecord(List<DbfField> fields)
		{
			this.fields = fields;
			data = new List<object>();
			foreach (DbfField field in fields) data.Add(null);
		}

		public List<object> Data {
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

		internal void Write(BinaryWriter writer)
		{
			// Write marker (always "not deleted")
			writer.Write((byte)0x20);

			int index = 0;
			foreach (DbfField field in fields)
			{
   			IEncoder encoder = EncoderFactory.GetEncoder(field.Type);
				byte[] buffer = encoder.Encode(field, data[index]);
				writer.Write(buffer);
				index++;
			}
		}
	}
}
