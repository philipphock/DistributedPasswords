using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedPasswordsWPF.model
{
    class Header
    {
        private string header = null;
        private static Random random = new Random();
        public Task<string> GenerateHeader()
        {
            Task<string> ret = Task.Run(() =>
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                return new string(Enumerable.Repeat(chars, 256).Select(s => s[random.Next(s.Length)]).ToArray());
            });

            return ret;
        }

        public Task<bool> EncryptHeader()
        {
            if (header == null)
            {
                throw new InvalidOperationException("Header not encrypted");
            }

            Task<bool> ret = Task.Run(() =>
            {
                return false;
            });

            return ret;

        }

        public Task<bool> DecryptHeader()
        {
            if (header == null)
            {
                throw new InvalidOperationException("Header not encrypted");
            }

            Task<bool> ret = Task.Run(() =>
            {
                return false;
            });

            return ret;
        }
    }
}
