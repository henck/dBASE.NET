using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dBASE.NET
{
	/// <summary>
	/// Not all types are currently implemented.
	/// </summary>
	public enum DbfFieldType
	{
#pragma warning disable 1591
		Character = 'C',
		Currency = 'Y',
		Numeric = 'N',
		Float = 'F',
		Date = 'D',
		DateTime = 'T',
		Double = 'B',
		Integer = 'I',
		Logical = 'L',
		Memo = 'M',
		General = 'G',
		Picture = 'P',
		NullFlags = '0'
#pragma warning restore 1591
	}
}
