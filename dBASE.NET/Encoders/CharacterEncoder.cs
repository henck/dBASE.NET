using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dBASE.NET.Encoders
{
	internal class CharacterEncoder: IEncoder
	{
		private static CharacterEncoder instance = null;

		private CharacterEncoder() { }

		public static CharacterEncoder Instance
		{
			get
			{
				if (instance == null) instance = new CharacterEncoder();
				return instance;
			}
		}

		public byte[] Encode(DbfField field, object data)
		{
			// Convert data to string. NULL is the empty string.
			string text = data == null ? "" : (string) data;
			// Pad string with spaces.
			while (text.Length < field.Length) text = text + " ";
			// Convert string to byte array.
			return Encoding.ASCII.GetBytes((string)text);
		}

        public object Decode(byte[] buffer, byte[] memoData)
        {
            string text = Encoding.ASCII.GetString(buffer).Trim();
            if (text.Length == 0) return null;
            return text;
        }
    }
}
