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
            // Emanuele Bonin 22/03/2025
            // For visualFoxPro DBF Table there are other 263 bytes to add to the header
			// that is the path to the dbc that belong the table (all 0x00 for no databases)
			bool isVFP = this.Version == DbfVersion.VisualFoxPro || this.Version == DbfVersion.VisualFoxProWithAutoIncrement;
            this.HeaderLength = (ushort)(32 + fields.Count * 32 + 1 + (isVFP ? 263 : 0));
			this.NumRecords = (uint)records.Count;
			this.RecordLength = 1;
			foreach (DbfField field in fields)
			{
				this.RecordLength += field.Length;
			}

            // Emanuele Bonin 24/03/2025
            // Manage Table Flag to indicate if the table has a Memo field
            HasMemoField = false;
            byte tableFlag = 0;
            if (isVFP && fields.Any(f => f.Type == DbfFieldType.Memo)) {
                HasMemoField = true;
                tableFlag |= 0x02;
			}

            writer.Write((byte)Version);					// 0x00 Version
			writer.Write((byte)(LastUpdate.Year - 1900));   // 0x01 AA
            writer.Write((byte)(LastUpdate.Month));         // 0x02 MM
            writer.Write((byte)(LastUpdate.Day));           // 0x03 DD
            writer.Write(NumRecords);                       // 0x04 - 0x007 Number of records
            writer.Write(HeaderLength);                     // 0x08 - 0x09 Position of first data record
            writer.Write(RecordLength);                     // 0x0A - 0x0B Length of one data record including delete flag
            for (int i = 0; i < 16; i++) writer.Write((byte)0); // 0x0C - 0x1B Reserved
															// Visual foxpro
            writer.Write((byte)tableFlag);					// 0x1C Table flags
															// Values:
															// 0x01 file has a structural .cdx
															// 0x02 file has a Memo field (.fpt file)
															// 0x04 file is a database (.dbc)
															// This byte can contain the sum of any of the above values.
															// For example, the value 0x03 indicates the table has a structural .cdx and a Memo field.
            for (int i = 0; i < 3; i++) writer.Write((byte)0);
		}
	}
}
