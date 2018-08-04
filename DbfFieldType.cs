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
	}
}
