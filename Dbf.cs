using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dBASE.NET
{
	/// <summary>
	/// Loading a .dbf file
	/// 
	/// using dBASE.NET;
	/// ...
	/// Dbf dbf = new Dbf("mydb.dbf");
	///
	/// Looping through fields
	/// foreach (DbfField field in dbf.Fields)
	/// {
	///   Console.WriteLine("Field name: " + field.name);
	/// }
	///
	/// Looping through records
	/// 
	/// foreach(DbfRecord record in dbf.Records) 
	/// {
	///   foreach (DbfField fld in dbf.Fields) {
	///     Console.WriteLine(record.Data[fld]);
	///   }		  
	/// }
	/// </summary>
	public class Dbf
	{
		private DbfHeader header;
		private List<DbfField> fields;
		private List<DbfRecord> records;

		public Dbf()
		{
			this.fields = new List<DbfField>();
			this.records = new List<DbfRecord>();
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

		public void Read(String path)
		{
			// Open stream for reading.
			FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read);
			BinaryReader reader = new BinaryReader(stream);

			ReadHeader(reader);
			byte[] memoData = ReadMemos(path);
			ReadFields(reader);

			// After reading the fields, we move the read pointer to the beginning
			// of the records, as indicated by the "HeaderLength" value in the header.
			stream.Seek(header.HeaderLength, SeekOrigin.Begin);

			ReadRecords(reader, memoData);

			// Close stream.
			reader.Close();
			stream.Close();
		}

		private byte[] ReadMemos(string path)
		{
			String memoPath = Path.ChangeExtension(path, "fpt");
			if (!File.Exists(memoPath))
			{
				memoPath = Path.ChangeExtension(path, "dbt");
				if(!File.Exists(memoPath))
				{
					return null;
				}
			}

			FileStream str = File.Open(memoPath, FileMode.Open, FileAccess.Read);
			BinaryReader memoReader = new BinaryReader(str);
			byte[] memoData = new byte[str.Length];
			memoData = memoReader.ReadBytes((int)str.Length);
			memoReader.Close();
			str.Close();
			return memoData;
		}

		private void ReadHeader(BinaryReader reader)
		{
			// Peek at version number, then try to read correct version header.
			byte versionByte = reader.ReadByte();
			reader.BaseStream.Seek(0, SeekOrigin.Begin);
			DbfVersion version = (DbfVersion)versionByte;
			header = DbfHeader.CreateHeader(version);
			header.Read(reader);
		}

		private void ReadFields(BinaryReader reader)
		{
			fields.Clear();

			// Fields are terminated by 0x0d char.
			while(reader.PeekChar() != 0x0d)
			{
				fields.Add(new DbfField(reader));
			}

			// Read fields terminator.
			reader.ReadByte();
		}

		private void ReadRecords(BinaryReader reader, byte[] memoData)
		{
			records.Clear();

			// Records are terminated by 0x1a char (officially), or EOF (also seen).
			while(reader.PeekChar() != 0x1a && reader.PeekChar() != -1)
			{
				records.Add(new DbfRecord(reader, header, fields, memoData));
			}
		}
	}
}
