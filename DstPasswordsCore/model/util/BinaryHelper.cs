using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DstPasswordsCore.model.util
{
    class BinaryHelper
    {
        public static byte[] Unhexlify(string hex)
        {
            var chars = hex.ToCharArray();
            var bytes = new List<byte>();
            for (int index = 0; index < chars.Length; index += 2)
            {
                var chunk = new string(chars, index, 2);
                bytes.Add(byte.Parse(chunk, System.Globalization.NumberStyles.AllowHexSpecifier));
            }
            return bytes.ToArray();
        }


    }
}
