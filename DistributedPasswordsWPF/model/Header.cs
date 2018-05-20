using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DistributedPasswordsWPF.model
{
    class Header
    {
        public string _header = null;
        private string _decryptedheader = null;

        private string _hash = null;
        private static Random random = new Random();

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
            return ""; //TODO
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
        

        public void DecryptHeader(string password)
        {
            ReadHeader();

            if (_header == null)
            {
                throw new InvalidOperationException("Header not decrypted");
            }
            this._decryptedheader = Crypto.Decrypt(this._header, EnhancePassword(password));
            Debug.WriteLine("decryptedHeader"+this._decryptedheader);
            
        }

        private void ReadHeader()
        {
            string file = Path.Combine(Settings.KEYS_PATH, "header");
            if (File.Exists(file))
            {
                this._header = File.ReadAllText(file);
                Debug.WriteLine("eHeader: " + this._header);
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
                //Debug.WriteLine("############# - Hash: "+this._hash);

            }
            else
            {
                //Debug.WriteLine("############# - Generate Hash");
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
