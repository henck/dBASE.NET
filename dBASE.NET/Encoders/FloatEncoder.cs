namespace dBASE.NET.Encoders
{
    using System;
    using System.Globalization;
    using System.Text;

    internal class FloatEncoder : IEncoder
    {
        private static FloatEncoder instance;

        private FloatEncoder() { }

        public static FloatEncoder Instance => instance ?? (instance = new FloatEncoder());

        /// <inheritdoc />
        public byte[] Encode(DbfField field, object data, Encoding encoding)
        {
            string text = Convert.ToString(data, CultureInfo.InvariantCulture).PadLeft(field.Length, ' ');
            if (text.Length > field.Length)
            {
                text.Substring(0, field.Length);
            }

            return encoding.GetBytes(text);
        }

        /// <inheritdoc />
        public object Decode(byte[] buffer, byte[] memoData, Encoding encoding)
        {
            string text = encoding.GetString(buffer).Trim();
            if (text.Length == 0)
            {
                return null;
            }

            return Convert.ToSingle(text, CultureInfo.InvariantCulture);
        }
    }
}