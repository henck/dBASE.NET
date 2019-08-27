namespace dBASE.NET.Encoders
{
    using System.Text;

    internal class NullFlagsEncoder : IEncoder
    {
        private static NullFlagsEncoder instance = null;

        private NullFlagsEncoder() { }

        public static NullFlagsEncoder Instance
        {
            get
            {
                if (instance == null) instance = new NullFlagsEncoder();
                return instance;
            }
        }

        /// <inheritdoc />
        public byte[] Encode(DbfField field, object data, Encoding encoding)
        {
            byte[] buffer = new byte[1];
            buffer[0] = 0;
            return buffer;
        }

        /// <inheritdoc />
        public object Decode(byte[] buffer, byte[] memoData, Encoding encoding)
        {
            return buffer[0];
        }
    }
}