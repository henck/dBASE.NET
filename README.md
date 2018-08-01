# dBASE.NET

_dBASE.NET_ is a simple .NET class library used to load dBASE IV .dbf files. The `Dbf` class reads
fields (`DbfField`) and records (`DbfRecord`) from a .dbf file. These fields and records can then
be accessed as lists and looped over.

## Sample of use

```c#
// Read a .dbf file into memory.
Dbf dbf = new Dbf("mydb.dbf");

// Loop through all fields:
foreach (DbfField field in dbf.Fields)
{
  Console.WriteLine(field.name);
}

// Loop through all records:
foreach(DbfRecord record in dbf.Records) 
{
  foreach (DbfField fld in dbf.Fields) {
    Console.WriteLine(record.Data[fld]);
  }		  
}
```
