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
			byte[] buffer = new byte[1];
			buffer[0] = 0;
			return buffer;
		}

        public object Decode(byte[] buffer, byte[] memoData)
        {
            return buffer[0];
        }
    }
}
