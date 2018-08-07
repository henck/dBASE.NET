using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dBASE.NET.Decoders
{
	internal class DateDecoder: IDecoder
	{
		private static DateDecoder instance = null;

		private DateDecoder() { }

		public static DateDecoder Instance
		{
			get
			{
				if (instance == null) instance = new DateDecoder();
				return instance;
			}
		}

		public object Decode(byte[] buffer, byte[] memoData)
		{
			string text = Encoding.ASCII.GetString(buffer).Trim();
			if (text.Length == 0) return null;
			return DateTime.ParseExact(text, "yyyyMMdd", CultureInfo.InvariantCulture);
		}
	}
}
