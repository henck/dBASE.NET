using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dBASE.NET.Decoders
{
	internal class LogicalDecoder : IDecoder
	{
		private static LogicalDecoder instance = null;

		private LogicalDecoder() { }

		public static LogicalDecoder Instance
		{
			get
			{
				if (instance == null) instance = new LogicalDecoder();
				return instance;
			}
		}

		public object Decode(byte[] buffer, byte[] memoData)
		{
			string text = Encoding.ASCII.GetString(buffer).Trim().ToUpper();
			if (text == "?") return null;
			return (text == "Y" || text == "T");
		}
	}
}
