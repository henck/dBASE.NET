using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace dBASE.NET
{
	public class Dbf3Header : DbfHeader
	{
		internal override void Read(BinaryReader reader)
		{
			Version = (DbfVersion) reader.ReadByte();
			var year = 1900 + reader.ReadByte();
			var month = reader.ReadByte();
			var day = reader.ReadByte();
			LastUpdate = new DateTime(year, month < 1 ? 1 : month, day < 1 ? 1 : day);
			NumRecords = reader.ReadUInt32();
			HeaderLength = reader.ReadUInt16();
			RecordLength = reader.ReadUInt16();
			reader.ReadBytes(20); // Skip rest of header.
		}

		internal override void Write(BinaryWriter writer, List<DbfField> fields, List<DbfRecord> records)
		{
			this.LastUpdate = DateTime.Now;
			// Header length = header fields (32b ytes)
			//               + 32 bytes for each field
      //               + field descriptor array terminator (1 byte)
			this.HeaderLength = (ushort)(32 + fields.Count * 32 + 1);
			this.NumRecords = (uint)records.Count;
			this.RecordLength = 1;
			foreach (DbfField field in fields)
			{
				this.RecordLength += field.Length;
			}

			writer.Write((byte)Version);
			writer.Write((byte)(LastUpdate.Year - 1900));
			writer.Write((byte)(LastUpdate.Month));
			writer.Write((byte)(LastUpdate.Day));
			writer.Write(NumRecords);
			writer.Write(HeaderLength);
			writer.Write(RecordLength);
			for (int i = 0; i < 20; i++) writer.Write((byte)0);
		}
	}
}
