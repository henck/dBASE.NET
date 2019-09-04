namespace dBASE.NET.Encoders
{
    using System;
    using System.Text;

    internal class MemoEncoder : IEncoder
    {
        private static MemoEncoder instance;

        private MemoEncoder() { }

        public static MemoEncoder Instance => instance ?? (instance = new MemoEncoder());

        /// <inheritdoc />
        public byte[] Encode(DbfField field, object data, Encoding encoding)
        {
            return null;
        }

        /// <inheritdoc />
        public object Decode(byte[] buffer, byte[] memoData, Encoding encoding)
        {
            int index = 0;
            // Memo fields of 5+ byts in length store their index in text, e.g. "     39394"
            // Memo fields of 4 bytes store their index as an int.
            if (buffer.Length > 4)
            {
                string text = encoding.GetString(buffer).Trim();
                if (text.Length == 0) return null;
                index = Convert.ToInt32(text);
            }
            else
            {
                index = BitConverter.ToInt32(buffer, 0);
                if (index == 0) return null;
            }

            return findMemo(index, memoData, encoding);
        }

        private static string findMemo(int index, byte[] memoData, Encoding encoding)
        {
            // This is the original implementation of findMemo. It was found that
            // the LINQ methods are orders of magnitude slower than using using array
            // offsets.

            /* UInt16 blockSize = BitConverter.ToUInt16(memoData.Skip(6).Take(2).Reverse().ToArray(), 0);
               int type = (int)BitConverter.ToUInt32(memoData.Skip(index * blockSize).Take(4).Reverse().ToArray(), 0);
               int length = (int)BitConverter.ToUInt32(memoData.Skip(index * blockSize + 4).Take(4).Reverse().ToArray(), 0);
               string text = encoding.GetString(memoData.Skip(index * blockSize + 8).Take(length).ToArray()).Trim();
               return text; */

            // The index is measured from the start of the file, even though the memo file header blocks takes
            // up the first few index positions.
            UInt16 blockSize = BitConverter.ToUInt16(new[] { memoData[7], memoData[6] }, 0);
            int length = (int)BitConverter.ToUInt32(
                new[]
                {
                    memoData[index * blockSize + 4 + 3],
                    memoData[index * blockSize + 4 + 2],
                    memoData[index * blockSize + 4 + 1],
                    memoData[index * blockSize + 4 + 0],
                },
                0);

            byte[] memoBytes = new byte[length];
            int lengthToSkip = index * blockSize + 8;

            for (int i = lengthToSkip; i < lengthToSkip + length; ++i)
            {
                memoBytes[i - lengthToSkip] = memoData[i];
            }

            return encoding.GetString(memoBytes).Trim();
        }
    }
}