using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace dBASE.NET.Tests
{
	/// <summary>
	/// DBase3WithMemo is version 0x83.
	/// </summary>
	[TestClass]
	public class DBase3WithMemo
	{
		Dbf dbf;

		[TestInitialize]
		public void testInit()
		{
			dbf = new Dbf();
			dbf.Read("fixtures/83/dbase_83.dbf");
		}

		[TestMethod]
		public void RecordCount()
		{
			Assert.AreEqual(67, dbf.Records.Count, "Should read 67 records");
		}

		[TestMethod]
		public void FieldCount()
		{
			Assert.AreEqual(15, dbf.Fields.Count, "Should read 15 fields");
		}
	}
}
