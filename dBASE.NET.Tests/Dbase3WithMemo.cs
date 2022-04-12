using System;
using Xunit;

namespace dBASE.NET.Tests
{
	/// <summary>
	/// DBase3WithMemo is version 0x83.
	/// </summary>
	
	public class DBase3WithMemo
	{
		Dbf dbf;

		public DBase3WithMemo()
		{
			dbf = new Dbf();
			dbf.Read("fixtures/83/dbase_83.dbf");
		}

		[Fact]
		public void RecordCount()
		{
			AssertX.Equal(67, dbf.Records.Count, "Should read 67 records");
		}

		[Fact]
		public void FieldCount()
		{
			AssertX.Equal(15, dbf.Fields.Count, "Should read 15 fields");
		}
	}
}
