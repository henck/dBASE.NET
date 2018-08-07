using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dBASE.NET.Encoders
{
	public class IntegerEncoder: IEncoder
	{
		private static IntegerEncoder instance = null;

		private IntegerEncoder() { }

		public static IntegerEncoder Instance
		{
			get
			{
				if (instance == null) instance = new IntegerEncoder();
				return instance;
			}
		}

		public byte[] Encode(DbfField field, object data)
		{
			return null;
		}
	}
}
