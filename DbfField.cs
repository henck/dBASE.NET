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
	/// Encapsulates a field descriptor in a .dbf file.
	/// </summary>
	public class DbfField
	{
		/// <summary>
		/// Field name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Field type
		/// </summary>
		public DbfFieldType Type { get; set; }

		/// <summary>
		/// Length of field in bytes
		/// </summary>
		public byte Length { get; set; }
		public byte Precision { get; set; }
		public byte WorkAreaID { get; set; }
		public byte Flags { get; set; }

		public DbfField(BinaryReader reader)
		{
			Name = Encoding.ASCII.GetString(reader.ReadBytes(11)).TrimEnd((Char)0);
			Type = (DbfFieldType) reader.ReadByte();
			reader.ReadBytes(4);
			Length = reader.ReadByte();
			Precision = reader.ReadByte();
			reader.ReadBytes(2); // reserved.
			WorkAreaID = reader.ReadByte();
			reader.ReadBytes(2); // reserved.
			Flags = reader.ReadByte();
			reader.ReadBytes(8);
		}
	}
}
