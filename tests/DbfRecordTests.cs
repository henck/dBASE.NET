using Xunit;

namespace dBASE.NET.Tests;

public class DbfRecordTests
{
    private readonly Dbf dbf;

    public DbfRecordTests()
    {
        dbf = new Dbf();
        dbf.Read("fixtures/83/dbase_83.dbf");
    }

    [Fact]
    public void ToStringTest()
    {
        Assert.Equal(
            "ID=87,CATCOUNT=2,AGRPCOUNT=0,PGRPCOUNT=0,ORDER=87,CODE=1,NAME=Assorted Petits Fours,THUMBNAIL=graphics/00000001/t_1.jpg,IMAGE=graphics/00000001/1.jpg,PRICE=0,COST=0,DESC=,WEIGHT=5.51,TAXABLE=True,ACTIVE=True",
            dbf.Records[0].ToString());
        Assert.Equal(
            "ID=87; CATCOUNT=2; AGRPCOUNT=0; PGRPCOUNT=0; ORDER=87; CODE=1; NAME=Assorted Petits Fours; THUMBNAIL=graphics/00000001/t_1.jpg; IMAGE=graphics/00000001/1.jpg; PRICE=0; COST=0; DESC=; WEIGHT=5.51; TAXABLE=True; ACTIVE=True",
            dbf.Records[0].ToString("; "));
        Assert.Equal(
            "ID->87; CATCOUNT->2; AGRPCOUNT->0; PGRPCOUNT->0; ORDER->87; CODE->1; NAME->Assorted Petits Fours; THUMBNAIL->graphics/00000001/t_1.jpg; IMAGE->graphics/00000001/1.jpg; PRICE->0; COST->0; DESC->; WEIGHT->5.51; TAXABLE->True; ACTIVE->True",
            dbf.Records[0].ToString("; ", "{name}->{value}"));
        Assert.Equal(
            "ID; CATCOUNT; AGRPCOUNT; PGRPCOUNT; ORDER; CODE; NAME; THUMBNAIL; IMAGE; PRICE; COST; DESC; WEIGHT; TAXABLE; ACTIVE",
            dbf.Records[0].ToString("; ", "{name}"));
        Assert.Equal(
            "87; 2; 0; 0; 87; 1; Assorted Petits Fours; graphics/00000001/t_1.jpg; graphics/00000001/1.jpg; 0; 0; ; 5.51; True; True",
            dbf.Records[0].ToString("; ", "{value}"));
    }
}