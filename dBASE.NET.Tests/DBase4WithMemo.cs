using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace dBASE.NET.Tests
{
	/// <summary>
	/// DBase4WithMemo is version 0x8b.
	/// </summary>
	[TestClass]
	public class DBase4WithMemo
	{
		Dbf dbf;

		[TestInitialize]
		public void testInit()
		{
			dbf = new Dbf();
			dbf.Read("fixtures/8b/dbase_8b.dbf");
		}

		[TestMethod]
		public void RecordCount()
		{
			Assert.AreEqual(10, dbf.Records.Count, "Should read 10 records");
		}

		[TestMethod]
		public void FieldCount()
		{
			Assert.AreEqual(6, dbf.Fields.Count, "Should read 6 fields");
		}
	}
}
