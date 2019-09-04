namespace dBASE.NET.Encoders
{
    using System;
    using System.Text;

    public class IntegerEncoder : IEncoder
    {
        private static IntegerEncoder instance;

        private IntegerEncoder() { }

        public static IntegerEncoder Instance => instance ?? (instance = new IntegerEncoder());

        /// <inheritdoc />
        public byte[] Encode(DbfField field, object data, Encoding encoding)
        {
            int value = 0;
            if (data != null) value = (int)data;
            return BitConverter.GetBytes(value);
        }

        /// <inheritdoc />
        public object Decode(byte[] buffer, byte[] memoData, Encoding encoding)
        {
            return BitConverter.ToInt32(buffer, 0);
        }
    }
}