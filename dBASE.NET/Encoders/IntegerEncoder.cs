﻿namespace dBASE.NET.Encoders {
    using System;
    using System.Text;

    public class IntegerEncoder : IEncoder {
        private static IntegerEncoder instance;

        private IntegerEncoder() { }

        public static IntegerEncoder Instance => instance ?? (instance = new IntegerEncoder());

        /// <inheritdoc />
        public byte[] Encode(DbfField field, object data, Encoding encoding) {
            int value = 0;
            if (data != null) {
                if (data is decimal) {
                    // Handle decimal to int conversion
                    // This is a workaround for the issue with decimal to int conversion
                    // in .NET where it throws an exception if the decimal is not a whole number
                    // and the value is not a valid integer.
                    value = Convert.ToInt32((decimal)data);

                } else if (data is string) {
                    if (!Int32.TryParse(data.ToString(), out value)) value = 0;
                } else if (data == null || data == DBNull.Value) {
                    value = 0;
                } else {
                    value = Convert.ToInt32(data);
                }
            }
            return BitConverter.GetBytes(value);
        }

        /// <inheritdoc />
        public object Decode(byte[] buffer, byte[] memoData, Encoding encoding) {
            return BitConverter.ToInt32(buffer, 0);
        }
    }
}