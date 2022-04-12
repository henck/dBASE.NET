using System;
using System.IO;
using Xunit;

namespace dBASE.NET.Tests
{
    /// <summary>
    /// DBase3WithMemo is version 0x83.
    /// </summary>
    
    public class Dbase3WithMemoStreamRead
    {
        Dbf dbf;

        public Dbase3WithMemoStreamRead()
        {
            dbf = new Dbf();

            using (FileStream baseStream = File.Open("fixtures/83/dbase_83.dbf", FileMode.Open, FileAccess.Read))
            using (FileStream memoStream = File.Open("fixtures/83/dbase_83.dbt", FileMode.Open, FileAccess.Read))
                dbf.Read(baseStream, memoStream);
        }

        [Fact]
        public void RecordCount()
        {
            AssertX.Equal(67, dbf.Records.Count, "Should read 67 records");
        }

        [Fact]
        public void FieldCount()
        {
            AssertX.Equal(15, dbf.Fields.Count, "Should read 15 fields");
        }
    }
}
