using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DistributedPasswordsWPF.model
{
    class Header
    {
        public string _header = null;
        private string _decryptedheader = null;

        private string _hash = null;
        private static Random random = new Random();
        private readonly Regex _headerRegex = new Regex("^[a-zA-Z0-9]*$");

        private string EnhancePassword(string password)
        {
            ReadHash();
            byte[] h = (new SHA256Managed()).ComputeHash(Encoding.UTF8.GetBytes(String.Concat(password, _hash)));
            return DecodeEncodeHelper.Bin2Hex(h);
        }
        
        public string EncryptWithHeaderPassword(string text)
        {
            return Crypto.Encrypt(text, _decryptedheader);
        }

        public string DecryptWithHeaderPassword(string text)
        {
            return Crypto.Decrypt(text, _decryptedheader);
        }

        private string GenerateHash()
        {
            byte[] rnd = new byte[4];
            random.NextBytes(rnd);
            string h = DecodeEncodeHelper.Bin2Hex(rnd);
            return h; 
        }
        private string GenerateHeader(string password)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string plainheader = new string(Enumerable.Repeat(chars, 256).Select(s => s[random.Next(s.Length)]).ToArray());
            return Crypto.Encrypt(plainheader, EnhancePassword(password));    

        }

        public void CreateHeader(string password)
        {
            string encryptedHeader = GenerateHeader(password);
            string p = Path.Combine(Settings.KEYS_PATH, "header").ToString();
            File.WriteAllText(p, encryptedHeader);
        }
        

        public bool DecryptHeader(string password)
        {
            ReadHeader();

            if (_header == null)
            {
                throw new InvalidOperationException("Header not decrypted");
            }
            string d = Crypto.Decrypt(this._header, EnhancePassword(password));
            DEBUG.Print(this.GetType(), "decryptedHeader: " + d);

            if (_headerRegex.IsMatch(d))
            {
                this._decryptedheader = d;            
                return true;
            }
            return false;
        }           
        

        private void ReadHeader()
        {
            string file = Path.Combine(Settings.KEYS_PATH, "header");
            if (File.Exists(file))
            {
                this._header = File.ReadAllText(file);
                DEBUG.Print(this.GetType(), "eHeader: " + this._header);
            }
            else
            {
                throw new InvalidOperationException("File not readable");
            }
        }
        private void ReadHash()
        {
            if (!string.IsNullOrEmpty(_hash))
            {
                //hash already read, do nothing
                return;
            }
            string file = Path.Combine(Settings.KEYS_PATH, "hash");
            if (File.Exists(file))
            {
                this._hash = File.ReadAllText(file);

            }
            else
            {
                DEBUG.Print(this.GetType(), "generate hash");
                _hash = GenerateHash();
                File.WriteAllText(Path.Combine(Settings.KEYS_PATH, "hash").ToString(), _hash);
            }
        }

        public bool IsHeaderFilePresent()
        {
            string file = Path.Combine(Settings.KEYS_PATH, "header");
            return File.Exists(file);
        }


        
        
    }
}
