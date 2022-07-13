namespace dBASE.NET.Tests
{
    using System.Collections.Generic;

    using Xunit;

    
    public class BrokenFields
    {
        private readonly List<DbfField> fields;

        public BrokenFields()
        {
            fields = new List<DbfField>
            {
                new("NUM", DbfFieldType.Numeric, 6),
                new("CODE", DbfFieldType.Character, 15),
                new("F", DbfFieldType.Character, 30),
                new("I", DbfFieldType.Character, 30),
                new("O", DbfFieldType.Character, 30),
                new("DOC", DbfFieldType.Character, 100),
                new("SERIES", DbfFieldType.Character, 7),
                new("NUMBER", DbfFieldType.Character, 7),
                new("DATAISSUE", DbfFieldType.Character, 200),
                new("ZIP", DbfFieldType.Numeric, 6),
                new("ADDRESS", DbfFieldType.Character, 200),
                new("SNILS", DbfFieldType.Character, 11),
                new("SUM", DbfFieldType.Numeric, 12, 2),
                new("MONTH", DbfFieldType.Character, 15),
                new("YEAR", DbfFieldType.Numeric, 4),
                new("NOTE", DbfFieldType.Character, 255),
                new("REASON", DbfFieldType.Character, 128),
                new("PERS_NUM", DbfFieldType.Character, 14)
            };
        }

        [Fact]
        public void ReadBrokenFieldsTest()
        {
            // Arrange.
            var dbf = ReadBrokenFields();

            // Assert.
            Assert.Equal(fields.Count, dbf.Fields.Count);
            for (int i = 0; i < fields.Count; i++)
            {
                Assert.Equal(fields[i], dbf.Fields[i]);
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