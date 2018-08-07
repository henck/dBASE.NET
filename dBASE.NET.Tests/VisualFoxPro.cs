using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace dBASE.NET.Tests
{
	/// <summary>
	/// VisualFoxPro is version 0x30.
	/// </summary>
	[TestClass]
	public class VisualFoxPro
	{
		private Dbf dbf;

		[TestInitialize]
		public void testInit()
		{
			dbf = new Dbf();
			dbf.Read("fixtures/30/dbase_30.dbf");
		}

		[TestMethod]
		public void RecordCount()
		{
			Assert.AreEqual(34, dbf.Records.Count, "Should read 34 records");
		}

		[TestMethod]
		public void FieldCount()
		{
			Assert.AreEqual(145, dbf.Fields.Count, "Should read 145 fields.");
		}

		[TestMethod]
		public void MemoTest()
		{
			Assert.AreEqual("Domestic Life\r\nWeddings", dbf.Records[0][10], "Wrong data in memo field");
		}

		[TestMethod]
		public void LastValue()
		{
			Assert.AreEqual("EE358272-5B1F-4373-8ED2-765047781352", dbf.Records[33][144], "Last field does not match");
		}

		[TestMethod]
		public void Date()
		{
			Assert.AreEqual(new DateTime(1999, 3, 5), dbf.Records[0][8], "Date does not match");
		}

		[TestMethod]
		public void NullDate()
		{
			Assert.AreEqual(null, dbf.Records[19][12], "Date does not match");
		}

		[TestMethod]
		public void Boolean()
		{
			Assert.AreEqual(false, dbf.Records[0][141], "Boolean should be false");
		}

		[TestMethod]
		public void DateTime()
		{
			Assert.AreEqual(new DateTime(2006,4,20,17,13,4), dbf.Records[0][137], "DateTime does not match");
		}
	}
}
