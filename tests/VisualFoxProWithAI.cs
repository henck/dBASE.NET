using System;
using Xunit;

namespace dBASE.NET.Tests
{
	/// <summary>
	/// VisualFoxProWithAI is version 0x31.
	/// </summary>
	
	public class VisualFoxProWithAI
	{
		private Dbf dbf;

		public VisualFoxProWithAI()
		{
			dbf = new Dbf();
			dbf.Read("fixtures/31/dbase_31.dbf");
		}

		[Fact]
		public void RecordCount()
		{
			AssertX.Equal(77, dbf.Records.Count, "Should read 77 records");
		}

		[Fact]
		public void FieldCount()
		{
			AssertX.Equal(11, dbf.Fields.Count, "Should read 11 fields.");
		}

		[Fact]
		public void TestFirstInteger()
		{
			AssertX.Equal(1, dbf.Records[0][0], "Integer is not equal to expected value.");
		}

		[Fact]
		public void TestLastInteger()
		{
			AssertX.Equal(77, dbf.Records[76][0], "Integer is not equal to expected value.");
		}
	}
}
