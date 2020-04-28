using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace dBASE.NET.Tests
{
    [TestClass]
    public class DbfComparerTests
    {
        [TestMethod]
        public void Given_Two_Dbf_Files_With_Same_Schema_And_Modified_Records_When_GetDiff_Of_Between_Two_Dbfs_Should_Then_Return_Changed_Records()
        {
            //Given
            string actualFilePath = "fixtures/DbfComparer/ModifiedSample/DiffExampleActual.dbf";
            string newFilePath = "fixtures/DbfComparer/ModifiedSample/DiffExampleNew.dbf";
            var actualDbf = new Dbf();
            var newDbf = new Dbf();
            actualDbf.Read(actualFilePath);
            newDbf.Read(newFilePath);
            //When
            var diff = actualDbf.GetDiff(newDbf);
            //Then
            Assert.AreEqual(2,diff.Count);
        }
        [TestMethod]
        public void Given_Two_Dbf_Files_With_Same_Schema_And_One_New_Record_When_GetDiff_Of_Between_Two_Dbfs_Should_Then_Return_Added_Record()
        {
            //Given
            string actualFilePath = "fixtures/DbfComparer/NewSample/DiffExampleActual.dbf";
            string newFilePath = "fixtures/DbfComparer/NewSample/DiffExampleNew.dbf";
            var actualDbf = new Dbf();
            var newDbf = new Dbf();
            actualDbf.Read(actualFilePath);
            newDbf.Read(newFilePath);
            //When
            var diff = actualDbf.GetDiff(newDbf);
            //Then
            Assert.AreEqual(4,diff.Count);
        }
        [TestMethod]
        public void Given_DbfRecordDiff_Object_When_Convert_Object_To_String_Should_Then_Return_String_With_Values_Changed()
        {
            //Given
            var diff = new DbfRecordDiff{
                RecordIndex = 1,
                State = DiffState.Modified,
                ColumnsChanged = new System.Collections.Generic.List<DbfColumnChange>{
                    new DbfColumnChange{
                        Field = new DbfField("Name",DbfFieldType.Character,128),
                        OldValue = "John",
                        NewValue = "Joel"
                    }
                }                
            };
            //, 'RUA SEBASTIAO MOREIRA, 136                        '
            string expectedDiffStr = "Index: 1, State: Modified\r\n  {\r\n    { Name, 'John', 'Joel'    }, \r\n  }\r\n";
            //When
            string diffStr = diff.ToString();            
            //Then
            Assert.AreEqual(expectedDiffStr,diffStr);
        }
    }
}