using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dBASE.NET.Decoders
{
	internal class DateTimeDecoder : IDecoder
	{
		private static DateTimeDecoder instance = null;

		private DateTimeDecoder() { }

		public static DateTimeDecoder Instance
		{
			get
			{
				if (instance == null) instance = new DateTimeDecoder();
				return instance;
			}
		}

		public object Decode(byte[] buffer, byte[] memoData)
		{
			return ConvertFoxProToDateTime(buffer);
		}

		private static DateTime ConvertFoxProToDateTime(byte[] buffer)
		{
			// DateTime is encoded as two double words.
			// The high word is the date, encoded as the number of days since Jan 1, 4713BC
			// The low word is the time, encoded as (Hours * 3600000) + (Minutes * 60000) + (Seconds * 1000)
			//  (the number of milliseconds since midnight).
			UInt32 dateWord = BitConverter.ToUInt32(buffer, 0);
			int timeWord = (int)BitConverter.ToUInt32(buffer, 4);

			// Convert date word to DateTime using Julian calendar
			DateTime date = JulianToDateTime(dateWord);

			// Get hour, minute, second from time word
			int hour = timeWord / 3600000;
			timeWord = timeWord - hour * 3600000;
			int minute = timeWord / 60000;
			timeWord = timeWord - minute * 60000;
			int second = timeWord / 1000;

			// Add time to DateTime
			return new DateTime(date.Year, date.Month, date.Day, hour, minute, second);
		}

		/// <summary>
		/// Convert a Julian Date as long to a .NET DateTime structure
		/// Implemented from pseudo code at http://en.wikipedia.org/wiki/Julian_day
		/// </summary>
		/// <param name="julianDateAsLong">Julian Date to convert (days since 01/01/4713 BC)</param>
		/// <returns>DateTime</returns>
		private static DateTime JulianToDateTime(long julianDateAsLong)
		{
			if (julianDateAsLong == 0) return DateTime.MinValue;
			double p = Convert.ToDouble(julianDateAsLong);
			double s1 = p + 68569;
			double n = Math.Floor(4 * s1 / 146097);
			double s2 = s1 - Math.Floor(((146097 * n) + 3) / 4);
			double i = Math.Floor(4000 * (s2 + 1) / 1461001);
			double s3 = s2 - Math.Floor(1461 * i / 4) + 31;
			double q = Math.Floor(80 * s3 / 2447);
			double d = s3 - Math.Floor(2447 * q / 80);
			double s4 = Math.Floor(q / 11);
			double m = q + 2 - (12 * s4);
			double j = (100 * (n - 49)) + i + s4;
			return new DateTime(Convert.ToInt32(j), Convert.ToInt32(m), Convert.ToInt32(d));
		}
	}
}
