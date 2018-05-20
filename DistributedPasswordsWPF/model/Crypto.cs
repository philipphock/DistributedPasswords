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

        public static string Encrypt(string plaintext, string password)
        {
            byte[] p = Encoding.UTF8.GetBytes(password);
            
            byte[] iv = new byte[16];

            rnd.NextBytes(iv);
            
            return DecodeEncodeHelper.Bin2Hex(AES.Encrypt(plaintext,p,iv));
        }

        public static string Decrypt(string cyphertext, string password)
        {
            
            byte[] t = DecodeEncodeHelper.Hex2Bin(cyphertext);
            byte[] p = Encoding.UTF8.GetBytes(password);
            var iv = t.Take(16);
            byte[] ivb = iv.ToArray();
            byte[] to = t.Skip(16).ToArray();
            Debug.WriteLine("pass: "+password);
            Debug.WriteLine("data: " +DecodeEncodeHelper.Bin2Hex(to));
            return AES.Decrypt(to, p, ivb);
        }
    }
}
