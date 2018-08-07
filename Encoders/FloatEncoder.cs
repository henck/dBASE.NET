using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dBASE.NET.Encoders
{
	internal class FloatEncoder: IEncoder
	{
		private static FloatEncoder instance = null;

		private FloatEncoder() { }

		public static FloatEncoder Instance
		{
			get
			{
				if (instance == null) instance = new FloatEncoder();
				return instance;
			}
		}

		public byte[] Encode(DbfField field, object data)
		{
			return null;
		}
	}
}
