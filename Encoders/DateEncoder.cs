using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dBASE.NET.Encoders
{
	internal class DateEncoder: IEncoder
	{
		private static DateEncoder instance = null;

		private DateEncoder() { }

		public static DateEncoder Instance
		{
			get
			{
				if (instance == null) instance = new DateEncoder();
				return instance;
			}
		}

		public byte[] Encode(DbfField field, object data)
		{
			return null;
		}
	}
}

