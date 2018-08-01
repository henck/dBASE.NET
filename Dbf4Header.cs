using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace dBASE.NET
{
	public class Dbf4Header : DbfHeader
	{
		public override void Read(BinaryReader reader)
		{
			Version = (DbfVersion) reader.ReadByte();
			LastUpdate = new DateTime(reader.ReadByte() + 1900, reader.ReadByte(), reader.ReadByte());
			NumRecords = reader.ReadUInt32();
			HeaderLength = reader.ReadUInt16();
			RecordLength = reader.ReadUInt16();
			reader.ReadBytes(20); // Skip rest of header.
		}
	}
}
