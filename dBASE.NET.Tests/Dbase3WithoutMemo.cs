using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace dBASE.NET.Tests
{
	/// <summary>
	/// DBase3WithoutMemo is version 0x03.
	/// </summary>
	[TestClass]
	public class DBase3WithoutMemo
	{
		Dbf dbf;

		[TestInitialize]
		public void testInit()
		{
			dbf = new Dbf();
			dbf.Read("fixtures/03/dbase_03.dbf");
		}

		[TestMethod]
		public void RecordCount()
		{
			Assert.AreEqual(14, dbf.Records.Count, "Should read 14 records");
		}

		[TestMethod]
		public void FieldCount()
		{
			Assert.AreEqual(31, dbf.Fields.Count, "Should read 31 fields");
		}

		[TestMethod]
		public void FieldNames()
		{
			Assert.AreEqual("Point_ID", dbf.Fields[0].Name, "First field name should be 'Point_ID'");
		}

		[TestMethod]
		public void FieldLength()
		{
			Assert.AreEqual(12, dbf.Fields[0].Length, "Point_ID field length must be 12.");
		}

		[TestMethod]
		public void FieldPrecision()
		{
			Assert.AreEqual(3, dbf.Fields[23].Precision, "GPS_Second field length must be 3.");
		}
	}
}
