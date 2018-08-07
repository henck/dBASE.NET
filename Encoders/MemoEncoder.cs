using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dBASE.NET.Encoders
{
	internal class MemoEncoder: IEncoder
	{
		private static MemoEncoder instance = null;

		private MemoEncoder() { }

		public static MemoEncoder Instance
		{
			get
			{
				if (instance == null) instance = new MemoEncoder();
				return instance;
			}
		}

		public byte[] Encode(DbfField field, object data)
		{
			return null;
		}
	}
}
