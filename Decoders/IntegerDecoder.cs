using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dBASE.NET.Decoders
{
	internal class IntegerDecoder: IDecoder
	{
		private static IntegerDecoder instance = null;

		private IntegerDecoder() { }

		public static IntegerDecoder Instance
		{
			get
			{
				if (instance == null) instance = new IntegerDecoder();
				return instance;
			}
		}

		public object Decode(byte[] buffer, byte[] memoData)
		{
			return BitConverter.ToInt32(buffer, 0);
		}
	}
}

