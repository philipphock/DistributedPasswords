using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedPasswordsWPF
{
    class Crypto
    {
        
        public static string Encrypt(string plaintext, string password)
        {
            byte[] b = Encoding.UTF8.GetBytes(plaintext);

            return "";
        }

        public static string Decrypt(string cyphertext, string password)
        {
            byte[] b = Encoding.UTF8.GetBytes(cyphertext);
            //byte[] iv = b.Take(64);
            //AES.Decrypt(b,)
            return "";
        }
    }
}
