namespace dBASE.NET.Encoders
{
    using System;
    using System.Text;

    internal class DateEncoder : IEncoder
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

        /// <inheritdoc />
        public byte[] Encode(DbfField field, object data, Encoding encoding)
        {
            string text = new string(' ', field.Length);
            if (data != null)
            {
                DateTime dt = (DateTime)data;
                text = String.Format("{0:d4}{1:d2}{2:d2}", dt.Year, dt.Month, dt.Day).PadLeft(field.Length, ' ');
            }

            return encoding.GetBytes(text);
        }

        /// <inheritdoc />
        public object Decode(byte[] buffer, byte[] memoData, Encoding encoding)
        {
            string text = encoding.GetString(buffer).Trim();
            if (text.Length == 0) return null;
            return DateTime.ParseExact(text, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}