using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace DstPasswordsCore.model
{
    public class Header
    {
        public string _header = null;
        private string _decryptedheader = null;

        private string _hash = null;
        private static Random random = new Random();
        private readonly Regex _headerRegex = new Regex("^[a-zA-Z0-9]*$");

        public void Clear()
        {
            _header = null;
            _hash = null;
            _decryptedheader = null;
        }

        public static string HASH_FULL_PATH()
        {
            return Path.Combine(PasswordSystem.Instance.Settings.KEYS_PATH, "hash").ToString();
        }
        public static string HEADER_FULL_PATH()
        {
            return Path.Combine(PasswordSystem.Instance.Settings.KEYS_PATH, "header").ToString();
        }

        private string EnhancePasswordOLD(string password)
        {
            ReadHash();
            byte[] h = (new SHA256Managed()).ComputeHash(Encoding.UTF8.GetBytes(String.Concat(password, _hash)));
            return DecodeEncodeHelper.Bin2Hex(h);
        }
        private string EnhancePassword(string password)
        {
            ReadHash();
            HMACSHA1 hmac = new HMACSHA1(Encoding.UTF8.GetBytes(password));
            byte[] h = hmac.ComputeHash(Encoding.UTF8.GetBytes(_hash));
                
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
            byte[] rnd = new byte[32];
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
            string p = Path.Combine(PasswordSystem.Instance.Settings.KEYS_PATH, "header").ToString();
            File.WriteAllText(p, encryptedHeader);
        }
        
        public void ChangePassword(string newp)
        {
            string newheader = Crypto.Encrypt(_decryptedheader, EnhancePassword(newp));
            
            File.WriteAllText(Header.HEADER_FULL_PATH(), newheader);

        }

        public bool DecryptHeader(string password, bool checkonly = false)
        {
            ReadHeader();

            if (_header == null)
            {
                throw new InvalidOperationException("Header not decrypted");
            }
            
            string d = Crypto.Decrypt(this._header, EnhancePassword(password));
            if (d == null) return false;

            if (_headerRegex.IsMatch(d))
            {
                if (!checkonly)
                {
                    this._decryptedheader = d;            
                }
                return true;
            }
            return false;
        }           
        

        private void ReadHeader()
        {
            string file = Path.Combine(PasswordSystem.Instance.Settings.KEYS_PATH, "header");
            if (File.Exists(file))
            {
                this._header = File.ReadAllText(file);
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
            string file = Path.Combine(PasswordSystem.Instance.Settings.KEYS_PATH, "hash");
            if (File.Exists(file))
            {
                this._hash = File.ReadAllText(file);

            }
            else
            {
                _hash = GenerateHash();
                File.WriteAllText(Path.Combine(PasswordSystem.Instance.Settings.KEYS_PATH, "hash").ToString(), _hash);
            }
        }

        public bool IsHeaderFilePresent()
        {
            string file = Path.Combine(PasswordSystem.Instance.Settings.KEYS_PATH, "header");
            return File.Exists(file);
        }


        
        
    }
}
