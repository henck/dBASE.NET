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
			switch(version)
			{
				case DbfVersion.FoxBaseDBase3NoMemo:
					return new Dbf3Header();
				case DbfVersion.VisualFoxPro:
					return new Dbf3Header();
				case DbfVersion.VisualFoxProWithAutoIncrement:
					return new Dbf3Header();
				case DbfVersion.FoxPro2WithMemo:
					return new Dbf3Header();
				case DbfVersion.FoxBaseDBase3WithMemo:
					return new Dbf3Header();
				case DbfVersion.dBase4WithMemo:
					return new Dbf3Header();
				default:
					throw new ArgumentException("Unsupported dBASE version: " + version);
			}
		}

		/// <summary>
		/// Read the .dbf file header. 
		/// </summary>
		/// <param name="reader"></param>
		public abstract void Read(BinaryReader reader);
	}
}
