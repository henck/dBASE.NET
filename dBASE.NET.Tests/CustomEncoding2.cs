namespace dBASE.NET.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CustomEncoding2
    {
        private readonly List<DbfField> fields;

        private readonly Dictionary<string, object> data;

        private readonly Encoding encoding;

        public CustomEncoding2()
        {
            fields = new List<DbfField>
            {
                new DbfField("中文字段", DbfFieldType.Character, 10),
                new DbfField("中文字段2", DbfFieldType.Character, 10),
            };

            data = new Dictionary<string, object>
            {
                { "中文字段", "股票代码" },
                { "中文字段2", "股票代码股票代码" },
            };

            encoding = Encoding.GetEncoding("GB2312");
        }

        [TestMethod]
        public void WriteGB2312()
        {
            var dbf = new Dbf(encoding);
            fields.ForEach(x => dbf.Fields.Add(x));

            DbfRecord record = dbf.CreateRecord();
            foreach (var field in fields)
            {
                object item = null;
                try
                {
                    item = data[field.Name];
                }
                catch (Exception)
                {
                    // ignored
                }

                record.Data[fields.IndexOf(field)] = item;
            }

            // Act.
            dbf.Write("test.dbf", DbfVersion.FoxBaseDBase3NoMemo);

            // Assert.
            var dbfTest = new Dbf(encoding);
            dbfTest.Read("test.dbf");
            var rowStd = dbfTest.Records[0];
            Assert.AreEqual(rowStd["中文字段"], "股票代码");
            Assert.AreEqual(rowStd["中文字段2"], "股票代码股");      //should be truncated to 10 bytes
        }
    }
}