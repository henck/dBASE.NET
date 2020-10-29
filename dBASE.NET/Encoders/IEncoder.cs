using System.Text;

namespace dBASE.NET.Encoders
{
    internal interface IEncoder
    {
        byte[] Encode(DbfField field, object data, Encoding encoding);

        object Decode(byte[] buffer, byte[] memoData, Encoding encoding);
    }
}