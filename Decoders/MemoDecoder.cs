using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dBASE.NET.Decoders
{
	internal class MemoDecoder : IDecoder
	{
		private static MemoDecoder instance = null;

		private MemoDecoder() { }

		public static MemoDecoder Instance
		{
			get
			{
				if (instance == null) instance = new MemoDecoder();
				return instance;
			}
		}

		public object Decode(byte[] buffer, byte[] memoData)
		{
			int index = BitConverter.ToInt32(buffer, 0);
			if (index != 0)
			{
				return findMemo(index, memoData);
			}
			else
			{
				return null;
			}
		}

		private string findMemo(int index, byte[] memoData)
		{
			// The index is measured from the start of the file, even though the memo file header blocks takes
			// up the first few index positions.
			UInt16 blockSize = BitConverter.ToUInt16(memoData.Skip(6).Take(2).Reverse().ToArray(), 0);
			int type = (int)BitConverter.ToUInt32(memoData.Skip(index * blockSize).Take(4).Reverse().ToArray(), 0);
			int length = (int)BitConverter.ToUInt32(memoData.Skip(index * blockSize + 4).Take(4).Reverse().ToArray(), 0);
			string text = Encoding.ASCII.GetString(memoData.Skip(index * blockSize + 8).Take(length).ToArray()).Trim();
			return text;
		}
	}
}
