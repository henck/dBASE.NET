namespace dBASE.NET.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CustomEncoding
    {
        private readonly List<DbfField> fields;

        private readonly Dictionary<string, object> data;

        private readonly Encoding encoding;

        public CustomEncoding()
        {
            fields = new List<DbfField>
            {
                new DbfField("OTD", DbfFieldType.Character, 4),
                new DbfField("FIL", DbfFieldType.Character, 5),
                new DbfField("SCHET", DbfFieldType.Character, 20),
                new DbfField("KV", DbfFieldType.Character, 2),
                new DbfField("SUMMA", DbfFieldType.Numeric, 10, 2),
                new DbfField("FAM", DbfFieldType.Character, 30),
                new DbfField("NAME", DbfFieldType.Character, 20),
                new DbfField("OTCH", DbfFieldType.Character, 20),
                new DbfField("DOC", DbfFieldType.Numeric, 2),
                new DbfField("DOCSER", DbfFieldType.Character, 10),
                new DbfField("DOCNUM", DbfFieldType.Character, 10),
                new DbfField("DOCVYD", DbfFieldType.Character, 50),
                new DbfField("DOCDATE", DbfFieldType.Date, 8),
                new DbfField("BDATE", DbfFieldType.Date, 8),
                new DbfField("TNOMER", DbfFieldType.Character, 7),
                new DbfField("KODI", DbfFieldType.Character, 2),
                new DbfField("KODZ", DbfFieldType.Numeric, 9, 2)
            };

            data = new Dictionary<string, object>
            {
                { "SCHET", "123456789" },
                { "KV", "00" },
                { "SUMMA", 1300.52 },
                { "FAM", "ПОРОШЕНКО" },
                { "NAME", "ПЕТР" },
                { "OTCH", "АЛЕКСЕЕВИЧ" },
                { "KODI", "02" },
            };

            encoding = Encoding.GetEncoding(866);
        }

        [TestMethod]
        public void ReadCP866()
        {
            // Arrange.
            var standard = ReadStandard();

            // Assert.
            var row = standard.Records[0];
            foreach (var field in fields)
            {
                var item = data.ContainsKey(field.Name) ? data[field.Name] : null;
                Assert.AreEqual(item, row[fields.IndexOf(field)]);
            }
        }

        [TestMethod]
        public void WriteCP866()
        {
            // Arrange.
            var standard = ReadStandard();

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
            var rowStd = standard.Records[0];
            var row = dbf.Records[0];
            foreach (var field in fields)
            {
                Assert.AreEqual(rowStd[field.Name], row[field.Name]);
            }
        }

        private Dbf ReadStandard()
        {
            var standard = new Dbf(encoding);
            standard.Read("fixtures/CP866/SPXXXX0159.dbf");

            return standard;
        }
    }
}