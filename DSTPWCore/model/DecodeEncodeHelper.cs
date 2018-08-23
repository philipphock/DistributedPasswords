using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DstPasswordsCore.model
{
    class DecodeEncodeHelper
    {
        public static byte[] Hex2Bin(string hexvalue)
        {
            if (hexvalue.Length % 2 != 0)
                hexvalue = "0" + hexvalue;
            int len = hexvalue.Length / 2;
            byte[] bytes = new byte[len];
            for (int i = 0; i < len; i++)
            {
                string byteString = hexvalue.Substring(2 * i, 2);
                bytes[i] = Convert.ToByte(byteString, 16);
            }
            return bytes;
        }

        public static string Bin2Hex(byte[] bytes)
        {
            StringBuilder hex = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }
}
