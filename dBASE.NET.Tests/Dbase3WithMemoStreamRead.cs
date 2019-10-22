using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace dBASE.NET.Tests
{
    /// <summary>
    /// DBase3WithMemo is version 0x83.
    /// </summary>
    [TestClass]
    public class Dbase3WithMemoStreamRead
    {
        Dbf dbf;

        [TestInitialize]
        public void testInit()
        {
            dbf = new Dbf();

            using (FileStream baseStream = File.Open("fixtures/83/dbase_83.dbf", FileMode.Open, FileAccess.Read))
            using (FileStream memoStream = File.Open("fixtures/83/dbase_83.dbt", FileMode.Open, FileAccess.Read))
                dbf.Read(baseStream, memoStream);
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
