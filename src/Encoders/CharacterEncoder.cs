namespace dBASE.NET.Encoders
{
    using System.Text;

    internal class CharacterEncoder : IEncoder
    {
        private static CharacterEncoder instance;

        private CharacterEncoder() { }

        public static CharacterEncoder Instance => instance ??= new CharacterEncoder();

        /// <inheritdoc />
        public byte[] Encode(DbfField field, object data, Encoding encoding)
        {
            // Input data maybe various: int, string, whatever.
            var res = data?.ToString();
            if (string.IsNullOrEmpty(res))
            {
                res = field.DefaultValue;
            }
            else
            {
                var resChars = res.ToCharArray();
                res = string.Empty;
                var resLength = 0;
                var charIndex = -1;
                while (resLength < field.Length)
                {
                    charIndex++;
                    if (charIndex < resChars.Length)
                    {
                        var resCharLength = encoding.GetBytes(resChars[charIndex].ToString()).Length;
                        if (resCharLength <= (field.Length - resLength))
                        {
                            res += resChars[charIndex];
                            resLength += resCharLength;
                            continue;
                        }
                    }

                    res += ' ';
                    resLength++;
                }
            }

            // Convert string to byte array.
            return encoding.GetBytes(res);
        }

        /// <inheritdoc />
        public object Decode(byte[] buffer, byte[] memoData, Encoding encoding)
        {
            var text = encoding.GetString(buffer).Trim();
            return text.Length == 0 ? null : text;
        }
    }
}