namespace dBASE.NET.Encoders
{
    using System.Text;

    internal class CharacterEncoder : IEncoder
    {
        private static CharacterEncoder instance;

        private CharacterEncoder() { }

        public static CharacterEncoder Instance => instance ?? (instance = new CharacterEncoder());

        /// <inheritdoc />
        public byte[] Encode(DbfField field, object data, Encoding encoding)
        {
            // Input data maybe various: int, string, whatever.
            string res = data?.ToString();
            if (string.IsNullOrEmpty(res))
            {
                res = field.DefaultValue;
            }
            else
            {
                // Pad string with spaces or trim.
                res = res.Length > field.Length
                    ? res.Substring(0, field.Length)
                    : res.PadRight(field.Length, ' ');
            }

            // Convert string to byte array.
            return encoding.GetBytes(res);
        }

        /// <inheritdoc />
        public object Decode(byte[] buffer, byte[] memoData, Encoding encoding)
        {
            string text = encoding.GetString(buffer).Trim();
            if (text.Length == 0) return null;
            return text;
        }
    }
}