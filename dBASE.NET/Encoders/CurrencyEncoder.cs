namespace dBASE.NET.Encoders
{
    using System;
    using System.Text;

    internal class CurrencyEncoder : IEncoder
    {
        private static CurrencyEncoder instance;

        private CurrencyEncoder() { }

        public static CurrencyEncoder Instance => instance ?? (instance = new CurrencyEncoder());

        /// <inheritdoc />
        public byte[] Encode(DbfField field, object data, Encoding encoding)
        {
            float value = 0;
            if (data != null) value = (float)data;
            return BitConverter.GetBytes(value);
        }

        /// <inheritdoc />
        public object Decode(byte[] buffer, byte[] memoData, Encoding encoding)
        {
            return BitConverter.ToSingle(buffer, 0);
        }
    }
}