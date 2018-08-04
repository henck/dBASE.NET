using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dBASE.NET.Decoders
{
	internal class NumericDecoder : IDecoder
	{
		private static NumericDecoder instance = null;

		private NumericDecoder() { }

		public static NumericDecoder Instance
		{
			get
			{
				if (instance == null) instance = new NumericDecoder();
				return instance;
			}
		}

		public object Decode(byte[] buffer, byte[] memoData)
		{
			string text = Encoding.ASCII.GetString(buffer).Trim();
			if (text.Length == 0) return null;
			return Convert.ToSingle(text);
		}
	}
}
