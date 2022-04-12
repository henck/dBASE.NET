using System;
using Xunit;

namespace dBASE.NET.Tests
{
	/// <summary>
	/// DBase4WithMemo is version 0x8b.
	/// </summary>
	
	public class DBase4WithMemo
	{
		Dbf dbf;

		public DBase4WithMemo()
		{
			dbf = new Dbf();
			dbf.Read("fixtures/8b/dbase_8b.dbf");
		}

		[Fact]
		public void RecordCount()
		{
			AssertX.Equal(10, dbf.Records.Count, "Should read 10 records");
		}

		[Fact]
		public void FieldCount()
		{
			AssertX.Equal(6, dbf.Fields.Count, "Should read 6 fields");
		}
	}
}
