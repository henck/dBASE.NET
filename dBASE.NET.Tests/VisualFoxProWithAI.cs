using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace dBASE.NET.Tests
{
	/// <summary>
	/// VisualFoxProWithAI is version 0x31.
	/// </summary>
	[TestClass]
	public class VisualFoxProWithAI
	{
		private Dbf dbf;

		[TestInitialize]
		public void testInit()
		{
			dbf = new Dbf();
			dbf.Read("fixtures/31/dbase_31.dbf");
		}

		[TestMethod]
		public void RecordCount()
		{
			Assert.AreEqual(77, dbf.Records.Count, "Should read 77 records");
		}

		[TestMethod]
		public void FieldCount()
		{
			Assert.AreEqual(11, dbf.Fields.Count, "Should read 11 fields.");
		}

		[TestMethod]
		public void TestFirstInteger()
		{
			Assert.AreEqual(1, dbf.Records[0][0], "Integer is not equal to expected value.");
		}

		[TestMethod]
		public void TestLastInteger()
		{
			Assert.AreEqual(77, dbf.Records[76][0], "Integer is not equal to expected value.");
		}
	}
}
