using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dBASE.NET.Decoders
{
	internal class NullFlagsDecoder: IDecoder
	{
		private static NullFlagsDecoder instance = null;

		private NullFlagsDecoder() { }

		public static NullFlagsDecoder Instance
		{
			get
			{
				if (instance == null) instance = new NullFlagsDecoder();
				return instance;
			}
		}

		public object Decode(byte[] buffer, byte[] memoData)
		{
			return buffer[0];
		}
	}
}