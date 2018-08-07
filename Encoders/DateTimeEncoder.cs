using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dBASE.NET.Encoders
{
	internal class DateTimeEncoder: IEncoder
	{
		private static DateTimeEncoder instance = null;

		private DateTimeEncoder() { }

		public static DateTimeEncoder Instance
		{
			get
			{
				if (instance == null) instance = new DateTimeEncoder();
				return instance;
			}
		}

		public byte[] Encode(DbfField field, object data)
		{
			if (field.Length != 8) throw new ArgumentException("DateTime fields must always be 8 bytes in length.");

			// Null values result in zeroes.
			if (data == null) return new byte[field.Length];

			// The date gets encoded as a Julian Day.
			DateTime dt = (DateTime)data;
			ulong date = DateToJulian(dt);

			// The time gets encoded as number of milliseconds since midnight.
			ulong time = (ulong)(dt.Hour * 3600000 + dt.Minute * 60000 + dt.Second * 1000);

			// Time is shifted to be the high double-word.
			for (int i = 0; i < 32; i++) time = time * 2;

			// Date and time are encoded as two uint32 double words.
			return BitConverter.GetBytes(date + time);
		}

		/// <summary>
		/// Convert a .NET DateTime structure to a Julian Day Number as long
		/// Implemented from pseudo code at http://en.wikipedia.org/wiki/Julian_day
		/// </summary>
		private static ulong DateToJulian(DateTime date)
		{
			ulong jdn = (ulong) ((1461 * (date.Year + 4800 + (date.Month - 14)/ 12))/ 4 + (367 * (date.Month - 2 - 12 * ((date.Month - 14)/ 12)))/ 12 - (3 * ((date.Year + 4900 + (date.Month - 14) / 12) / 100))/ 4 + date.Day - 32075);
			return jdn;
		}
	}
}
