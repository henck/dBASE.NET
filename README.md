# dBASE.NET - Read and write DBF files with .NET

__dBASE.NET__ is a .NET class library used to read FoxBase, dBASE III and dBASE IV .dbf files. Data read
from a file is returned as a list of typed fields and a list of records. This library is useful to add
data import from dBASE sources to a .NET project.

This code has been tested against a number of dBASE files found in the wild, including FoxBase and dBASE III/IV
files with and without memo files. A .NET unit test project is part of this repository and new test files
may be added to it over time.

## Installing dBASE.NET

...

## Introduction

### Opening a DBF file

```c#
using dBASE.NET;

dbf.Read("database.dbf");
```

This returns an instance of the `Dbf` class. With this, you can iterate over fields found in the table:

```c#
foreach(DbfField field in dbf.Fields) {
	Console.WriteLine(field.Name);
}
```

You can also iterate over records:

```c#
foreach(DbfRecord record in dbf.Records) {
	for(int i = 0;  i < dbf.Fields.Count; i++) {
		Console.WriteLine(record[i]);
	}
}
```

Count the records:

```c#
Console.WriteLine(dbf.Records.Count);
```


## Class diagram

![Class diagram](http://yuml.me/1cc9f823.png)

_yuml:_

```
http://yuml.me/diagram/scruffy/class/edit/[Dbf]+->[DbfRecord], [Dbf]+->[DbfField], [DbfRecord]+->[DbfField], [Dbf]->[DbfHeader], [DbfHeader]^-[Dbf4Header]
```` 
