# dBASE.NET

_dBASE.NET_ is a simple .NET class library used to load dBASE IV .dbf files. The `Dbf` class reads
fields (`DbfField`) and records (`DbfRecord`) from a .dbf file. These fields and records can then
be accessed as lists and looped over.

## Sample of use

### Loading a .dbf file

```c#
using dBASE.NET;
...
Dbf dbf = new Dbf("mydb.dbf");
```

### Looping through fields

```c#
foreach (DbfField field in dbf.Fields)
{
  Console.WriteLine("Field name: " + field.name);
}
```

### Looping through records

```c#
foreach(DbfRecord record in dbf.Records) 
{
  foreach (DbfField fld in dbf.Fields) {
    Console.WriteLine(record.Data[fld]);
  }		  
}
```
