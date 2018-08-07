using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dBASE.NET.Encoders
{
	internal class NumericEncoder: IEncoder
	{
		private static NumericEncoder instance = null;

		private NumericEncoder() { }

		public static NumericEncoder Instance
		{
			get
			{
				if (instance == null) instance = new NumericEncoder();
				return instance;
			}
		}

		public byte[] Encode(DbfField field, object data)
		{
			return null;
		}
	}
}