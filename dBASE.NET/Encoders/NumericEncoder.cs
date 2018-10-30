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
      string text = Convert.ToString(data, System.Globalization.CultureInfo.InvariantCulture).PadLeft(field.Length, ' ');      
			if (text.Length > field.Length) text.Substring(0, field.Length);
			return Encoding.ASCII.GetBytes(text);
		}

        public object Decode(byte[] buffer, byte[] memoData)
        {
            string text = Encoding.ASCII.GetString(buffer).Trim();
            if (text.Length == 0) return null;
            return Convert.ToSingle(text, System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}