using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dBASE.NET.Decoders
{
	internal class CurrencyDecoder: IDecoder
	{
		private static CurrencyDecoder instance = null;

		private CurrencyDecoder() { }

		public static CurrencyDecoder Instance
		{
			get
			{
				if (instance == null) instance = new CurrencyDecoder();
				return instance;
			}
		}

		public object Decode(byte[] buffer, byte[] memoData)
		{
			return BitConverter.ToSingle(buffer, 0);
		}
	}
}
