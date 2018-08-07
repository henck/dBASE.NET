using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dBASE.NET
{
	/// <summary>
	/// The DbfHeader is an abstract base class for headers of different
	/// flavors of dBASE files.
	/// </summary>
	public abstract class DbfHeader
	{
		/// <summary>
		/// dBASE version
		/// </summary>
		public DbfVersion Version { get; set; }

		/// <summary>
		/// Date of last update.
		/// </summary>
		public DateTime LastUpdate { get; set;  }

		/// <summary>
		/// Number of records in the file.
		/// </summary>
		public uint NumRecords { get; set; }

		/// <summary>
		///  Header length in bytes. The records start at this offset in the .dbf file.
		/// </summary>
		public ushort HeaderLength { get; set; }

		/// <summary>
		/// Record length in bytes.
		/// </summary>
		public ushort RecordLength { get; set; }

		public static DbfHeader CreateHeader(DbfVersion version)
		{
			DbfHeader header;
			switch(version)
			{
				case DbfVersion.FoxBaseDBase3NoMemo:
					header = new Dbf3Header();
					break;
				case DbfVersion.VisualFoxPro:
					header = new Dbf3Header();
					break;
				case DbfVersion.VisualFoxProWithAutoIncrement:
					header = new Dbf3Header();
					break;
				case DbfVersion.FoxPro2WithMemo:
					header = new Dbf3Header();
					break;
				case DbfVersion.FoxBaseDBase3WithMemo:
					header = new Dbf3Header();
					break;
				case DbfVersion.dBase4WithMemo:
					header = new Dbf3Header();
					break;
				default:
					throw new ArgumentException("Unsupported dBASE version: " + version);
			}

			header.Version = version;
			return header;
		}

		/// <summary>
		/// Read the .dbf file header from the specified reader.
		/// </summary>
		internal abstract void Read(BinaryReader reader);

		/// <summary>
		/// Write a .dbf file header to the specified writer.
		/// </summary>
		internal abstract void Write(BinaryWriter writer, List<DbfField> fields, List<DbfRecord> records);
	}
}
