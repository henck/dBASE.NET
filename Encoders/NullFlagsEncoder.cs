using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dBASE.NET.Encoders
{
  internal	class NullFlagsEncoder: IEncoder
	{
		private static NullFlagsEncoder instance = null;

		private NullFlagsEncoder() { }

		public static NullFlagsEncoder Instance
		{
			get
			{
				if (instance == null) instance = new NullFlagsEncoder();
				return instance;
			}
		}

		public byte[] Encode(DbfField field, object data)
		{
			return null;
		}
	}
}
