using DistributedPasswordsWPF.model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedPasswordsWPF
{
    class Crypto
    {
        private static readonly Random rnd = new Random();

        //def _pad(self, s):
        //return s + (self.bs - len(s) % self.bs) * chr(self.bs - len(s) % self.bs).encode()

        //def _unpad(s) :
        //return s[:-ord(s[len(s) - 1:])]


        private static string _unpad(string s)
        {
            try
            {
                int i = (int)s[s.Length - 1];
                string r = s.Substring(0, s.Length - i);
                return r;
            }
            catch(ArgumentOutOfRangeException)
            {
                //something went wrong during decryption
                return null;
            }
            
        }

        private static string _pad(string s)
        {
            int len = (32 - s.Length % 32);
            string s2 = new string((char)len, len);

            return string.Concat(s, s2);
        }

        public static string Encrypt(string plaintext, string password)
        {
            byte[] p = Encoding.UTF8.GetBytes(password);
            
            byte[] iv = new byte[16];

            rnd.NextBytes(iv);

            byte[] encrypted = AES.Encrypt(_pad(plaintext), p, iv);
            byte[] ivEnc = new byte[encrypted.Length + iv.Length];

            Array.Copy(iv, ivEnc, iv.Length);
            Array.Copy(encrypted, 0, ivEnc, iv.Length, encrypted.Length);

            return DecodeEncodeHelper.Bin2Hex(ivEnc);
        }

        public static string Decrypt(string cyphertext, string password)
        {
            
            byte[] t = DecodeEncodeHelper.Hex2Bin(cyphertext);
            byte[] p = Encoding.UTF8.GetBytes(password);
            var iv = t.Take(16);
            byte[] ivb = iv.ToArray();
            byte[] to = t.Skip(16).ToArray();
            //DEBUG.Print("Crypto", "pass: " + password);
            //DEBUG.Print("Crypto", "data: " + DecodeEncodeHelper.Bin2Hex(to));
            return _unpad(AES.Decrypt(to, p, ivb));
        }
    }
}
