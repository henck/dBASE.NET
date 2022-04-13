using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace dBASE.NET.Tests;

public class CustomEncoding
{
    private readonly List<DbfField> fields;
    private readonly Dictionary<string, object> data;
    private readonly Encoding encoding;
    private readonly Dbf standardDbf;

    public CustomEncoding()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        
        fields = new List<DbfField>
        {
            new ("OTD", DbfFieldType.Character, 4),
            new ("FIL", DbfFieldType.Character, 5),
            new ("SCHET", DbfFieldType.Character, 20),
            new ("KV", DbfFieldType.Character, 2),
            new ("SUMMA", DbfFieldType.Numeric, 10, 2),
            new ("FAM", DbfFieldType.Character, 30),
            new ("NAME", DbfFieldType.Character, 20),
            new ("OTCH", DbfFieldType.Character, 20),
            new ("DOC", DbfFieldType.Numeric, 2),
            new ("DOCSER", DbfFieldType.Character, 10),
            new ("DOCNUM", DbfFieldType.Character, 10),
            new ("DOCVYD", DbfFieldType.Character, 50),
            new ("DOCDATE", DbfFieldType.Date, 8),
            new ("BDATE", DbfFieldType.Date, 8),
            new ("TNOMER", DbfFieldType.Character, 7),
            new ("KODI", DbfFieldType.Character, 2),
            new ("KODZ", DbfFieldType.Numeric, 9, 2)
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
        
        standardDbf = new Dbf(encoding);
        standardDbf.Read("fixtures/CP866/SPXXXX0159.dbf");
    }

    [Fact]
    public void ReadCP866()
    {
        // Assert.
        var row = standardDbf.Records[0];
        foreach (var field in fields)
        {
            var item = data.ContainsKey(field.Name) ? data[field.Name] : null;
            Assert.Equal(item, row[fields.IndexOf(field)]);
        }
    }

    [Fact]
    public void WriteCP866()
    {
        // Arrange.
        var dbf = new Dbf(encoding);
        fields.ForEach(x => dbf.Fields.Add(x));

        var record = dbf.CreateRecord();
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
        dbf.Write("test_custom_encoding.dbf", DbfVersion.FoxBaseDBase3NoMemo);

        // Assert.
        var rowStd = standardDbf.Records[0];
        var row = dbf.Records[0];
        foreach (var field in fields)
        {
            Assert.Equal(rowStd[field.Name], row[field.Name]);
        }
    }
}