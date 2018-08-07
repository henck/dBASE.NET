using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dBASE.NET.Encoders
{
	internal class DateTimeEncoder: IEncoder
	{
		private static DateTimeEncoder instance = null;

		private DateTimeEncoder() { }

		public static DateTimeEncoder Instance
		{
			get
			{
				if (instance == null) instance = new DateTimeEncoder();
				return instance;
			}
		}

		public byte[] Encode(DbfField field, object data)
		{
			return null;
		}
	}
}
