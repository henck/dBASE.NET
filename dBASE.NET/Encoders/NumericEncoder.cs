namespace dBASE.NET.Encoders
{
    using System;
    using System.Text;

    internal class NumericEncoder : IEncoder
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

        /// <inheritdoc />
        public byte[] Encode(DbfField field, object data, Encoding encoding)
        {
            string text = Convert.ToString(data, System.Globalization.CultureInfo.InvariantCulture).PadLeft(field.Length, ' ');
            if (text.Length > field.Length) text.Substring(0, field.Length);
            return encoding.GetBytes(text);
        }

        /// <inheritdoc />
        public object Decode(byte[] buffer, byte[] memoData, Encoding encoding)
        {
            string text = encoding.GetString(buffer).Trim();
            if (text.Length == 0) return null;
            return Convert.ToDouble(text, System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}