namespace dBASE.NET.Encoders
{
    using System.Text;

    internal class CharacterEncoder : IEncoder
    {
        private static CharacterEncoder instance = null;

        private CharacterEncoder() { }

        public static CharacterEncoder Instance => instance ?? (instance = new CharacterEncoder());

        /// <inheritdoc />
        public byte[] Encode(DbfField field, object data, Encoding encoding)
        {
            // Convert data to string. NULL is the empty string.
            string text = data == null ? "" : (string)data;
            // Pad string with spaces.
            while (text.Length < field.Length) text = text + " ";
            // Convert string to byte array.
            return encoding.GetBytes((string)text);
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