using System;
using Xunit;

namespace dBASE.NET.Tests
{
	/// <summary>
	/// DBase3WithoutMemo is version 0x03.
	/// </summary>
	
	public class DBase3WithoutMemo
	{
		Dbf dbf;

		public DBase3WithoutMemo()
		{
			dbf = new Dbf();
			dbf.Read("fixtures/03/dbase_03.dbf");
		}

		[Fact]
		public void RecordCount()
		{
			AssertX.Equal(14, dbf.Records.Count, "Should read 14 records");
		}

		[Fact]
		public void FieldCount()
		{
			AssertX.Equal(31, dbf.Fields.Count, "Should read 31 fields");
		}

		[Fact]
		public void FieldNames()
		{
			AssertX.Equal("Point_ID", dbf.Fields[0].Name, "First field name should be 'Point_ID'");
		}

		[Fact]
		public void FieldLength()
		{
			AssertX.Equal(12, dbf.Fields[0].Length, "Point_ID field length must be 12.");
		}

		[Fact]
		public void FieldPrecision()
		{
			AssertX.Equal(3, dbf.Fields[23].Precision, "GPS_Second field length must be 3.");
		}
	}
}
