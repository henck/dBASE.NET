namespace dBASE.NET.Tests
{
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BrokenFields
    {
        private readonly List<DbfField> fields;

        public BrokenFields()
        {
            fields = new List<DbfField>
            {
                new DbfField("NUM", DbfFieldType.Numeric, 6),
                new DbfField("CODE", DbfFieldType.Character, 15),
                new DbfField("F", DbfFieldType.Character, 30),
                new DbfField("I", DbfFieldType.Character, 30),
                new DbfField("O", DbfFieldType.Character, 30),
                new DbfField("DOC", DbfFieldType.Character, 100),
                new DbfField("SERIES", DbfFieldType.Character, 7),
                new DbfField("NUMBER", DbfFieldType.Character, 7),
                new DbfField("DATAISSUE", DbfFieldType.Character, 200),
                new DbfField("ZIP", DbfFieldType.Numeric, 6),
                new DbfField("ADDRESS", DbfFieldType.Character, 200),
                new DbfField("SNILS", DbfFieldType.Character, 11),
                new DbfField("SUM", DbfFieldType.Numeric, 12, 2),
                new DbfField("MONTH", DbfFieldType.Character, 15),
                new DbfField("YEAR", DbfFieldType.Numeric, 4),
                new DbfField("NOTE", DbfFieldType.Character, 255),
                new DbfField("REASON", DbfFieldType.Character, 128),
                new DbfField("PERS_NUM", DbfFieldType.Character, 14)
            };
        }

        [TestMethod]
        public void ReadBrokenFieldsTest()
        {
            // Arrange.
            var dbf = ReadBrokenFields();

            // Assert.
            Assert.AreEqual(fields.Count, dbf.Fields.Count);
            for (int i = 0; i < fields.Count; i++)
            {
                Assert.AreEqual(fields[i], dbf.Fields[i]);
            }
        }

        private Dbf ReadBrokenFields()
        {
            var dbf = new Dbf();
            dbf.Read("fixtures/fields/POST986.dbf");

            return dbf;
        }
    }
}