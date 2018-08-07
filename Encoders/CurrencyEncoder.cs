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
			return null;
		}
	}
}