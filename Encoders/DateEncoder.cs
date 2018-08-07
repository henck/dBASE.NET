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
			string text = new string(' ', field.Length);
			if (data != null)
			{
				DateTime dt = (DateTime) data;
				text = String.Format("{0:d4}{1:d2}{2:d2}", dt.Year, dt.Month, dt.Day).PadLeft(field.Length, ' ');
			}

			return Encoding.ASCII.GetBytes(text);
		}
	}
}

