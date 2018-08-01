using dBASE.NET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dBASE.NET
{
	/// <summary>
	/// Reads a field record from a .dbf file.
	/// </summary>
	public class DbfField
	{
		public string Name { get; set; }
		public DbfFieldType Type { get; set; }
		public byte Length { get; set; }
		public byte Precision { get; set; }

		public DbfField(BinaryReader reader)
		{
			Name = Encoding.ASCII.GetString(reader.ReadBytes(11)).TrimEnd((Char)0);
			Type = (DbfFieldType) reader.ReadByte();
			reader.ReadBytes(4);
			Length = reader.ReadByte();
			Precision = reader.ReadByte();
			reader.ReadBytes(14);
		}
	}
}
