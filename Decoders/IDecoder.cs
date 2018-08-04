using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dBASE.NET.Decoders
{
	internal interface IDecoder
	{
		object Decode(byte[] buffer, byte[] memoData);
	}
}
