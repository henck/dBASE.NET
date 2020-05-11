using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace dBASE.NET
{
    public static class Utils
    {
        public static byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }        
    }
}
