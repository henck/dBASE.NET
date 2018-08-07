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

		public DbfField(string name, DbfFieldType type, byte length)
		{
			this.Name = name;
			this.Type = type;
			this.Length = length;
			this.Precision = 0;
			this.WorkAreaID = 0;
			this.Flags = 0;
		}

		internal DbfField(BinaryReader reader)
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

		internal void Write(BinaryWriter writer)
		{
			// Pad field name with 0-bytes, then save it.
			string name = this.Name;
			if (name.Length > 11) name = name.Substring(0, 11);
			while (name.Length < 11) name += '\0';
			byte[] nameBytes = Encoding.ASCII.GetBytes(name);
			writer.Write(nameBytes);

			writer.Write((char)Type);
			writer.Write((uint)0); // 4 reserved bytes.
			writer.Write(Length);
			writer.Write(Precision);

			for (int i = 0; i < 14; i++) writer.Write((byte)0); // 14 reserved bytes.
		}
	}
}
