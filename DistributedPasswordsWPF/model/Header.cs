using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedPasswordsWPF.model
{
    class Header
    {
        private string _header = null;
        private string _hash = null;
        private static Random random = new Random();

        

        private string GenerateHeader(string password)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string plainheader = new string(Enumerable.Repeat(chars, 256).Select(s => s[random.Next(s.Length)]).ToArray());
            return Crypto.Encrypt(plainheader, password);    

        }

        

        public Task<bool> DecryptHeader()
        {
            if (_header == null)
            {
                throw new InvalidOperationException("Header not encrypted");
            }

            Task<bool> ret = Task.Run(() =>
            {
                return false;
            });

            return ret;
        }

        private void ReadHeader()
        {
            string file = Path.Combine(Settings.KEYS_PATH, "header");
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
            string file = Path.Combine(Settings.KEYS_PATH, "hash");
            if (File.Exists(file))
            {
                this._hash = File.ReadAllText(file);

            }
            else
            {
                throw new InvalidOperationException("File not readable");
            }
        }

        public bool IsHeaderFilePresent()
        {
            string file = Path.Combine(Settings.KEYS_PATH, "header");
            return File.Exists(file);
        }

        public string getHeader(string password){
            if (_header == null)
            {
                Debug.WriteLine("header not cached");
                try
                {
                    Debug.WriteLine("try reading header from file");

                    ReadHeader();
                }
                catch (InvalidOperationException)
                {
                    Debug.WriteLine("no file exist, generate header");

                    _header = GenerateHeader(password);
                }

            }
            else
            {
                Debug.WriteLine("header cached");
            }

                return _header;
            }

    }
}
