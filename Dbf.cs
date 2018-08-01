using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dBASE.NET
{
	// Usage:
	// Dbf dbf = new Dbf("mydb.dbf");
	// foreach(DbfRecord record in dbf.Records) {
	//   record...
	// }

	public class Dbf
	{
		private DbfHeader header;
		private List<DbfField> fields;
		private List<DbfRecord> records;

		public Dbf(String path)
		{
			// Open stream for reading.
			FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read);
			BinaryReader reader = new BinaryReader(stream);

			ReadHeader(reader);
			ReadFields(reader);
			ReadRecords(reader);

			// Close stream.
			reader.Close();
			stream.Close();
		}

		public List<DbfField> Fields
		{
			get
			{
				return fields;
			}
		}

		public List<DbfRecord> Records
		{
		  get
			{
				return records;
			}
	  }

		private void ReadHeader(BinaryReader reader)
		{
			// Peek at version number, then try to read correct version header.
			DbfVersion version = (DbfVersion)reader.PeekChar();
			header = DbfHeader.CreateHeader(version);
			header.Read(reader);
		}

		private void ReadFields(BinaryReader reader)
		{
			fields = new List<DbfField>();

			// Fields are terminated by 0x0d char.
			while(reader.PeekChar() != 0x0d)
			{
				fields.Add(new DbfField(reader));
			}

			// Read fields terminator.
			reader.ReadByte();
		}

		private void ReadRecords(BinaryReader reader)
		{
			records = new List<DbfRecord>();

			// Records are terminated by 0x1a char (officially), or EOF (also seen).
			while(reader.PeekChar() != 0x1a && reader.PeekChar() != -1)
			{
				records.Add(new DbfRecord(reader, header, fields));
			}
		}
	}
}
