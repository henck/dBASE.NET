using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dBASE.NET.Encoders
{
	internal class CurrencyEncoder: IEncoder
	{
		private static CurrencyEncoder instance = null;

		private CurrencyEncoder() { }

		public static CurrencyEncoder Instance
		{
			get
			{
				if (instance == null) instance = new CurrencyEncoder();
				return instance;
			}
		}

		public byte[] Encode(DbfField field, object data)
		{
			float value = 0;
			if (data != null) value = (float)data;
			return BitConverter.GetBytes(value);
		}

        public object Decode(byte[] buffer, byte[] memoData)
        {
            return BitConverter.ToSingle(buffer, 0);
        }
    }
}