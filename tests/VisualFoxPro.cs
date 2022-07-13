using System;
using System.Text;
using System.Collections.Generic;
using Xunit;

namespace dBASE.NET.Tests
{
	/// <summary>
	/// VisualFoxPro is version 0x30.
	/// </summary>
	
	public class VisualFoxPro
	{
		private Dbf dbf;

		public VisualFoxPro()
		{
			dbf = new Dbf();
			dbf.Read("fixtures/30/dbase_30.dbf");
		}

		[Fact]
		public void RecordCount()
		{
			AssertX.Equal(34, dbf.Records.Count, "Should read 34 records");
		}

		[Fact]
		public void FieldCount()
		{
			AssertX.Equal(145, dbf.Fields.Count, "Should read 145 fields.");
		}

		[Fact]
		public void MemoTest()
		{
			AssertX.Equal("Domestic Life\r\nWeddings", dbf.Records[0][10], "Wrong data in memo field");
		}

		[Fact]
		public void LastValue()
		{
			AssertX.Equal("EE358272-5B1F-4373-8ED2-765047781352", dbf.Records[33][144], "Last field does not match");
		}

		[Fact]
		public void Date()
		{
			AssertX.Equal(new DateTime(1999, 3, 5), dbf.Records[0][8], "Date does not match");
		}

		[Fact]
		public void NullDate()
		{
			AssertX.Equal(null, dbf.Records[19][12], "Date does not match");
		}

		[Fact]
		public void Boolean()
		{
			AssertX.Equal(false, dbf.Records[0][141], "Boolean should be false");
		}

		[Fact]
		public void DateTime()
		{
			AssertX.Equal(new DateTime(2006,4,20,17,13,4), dbf.Records[0][137], "DateTime does not match");
		}
	}
}
