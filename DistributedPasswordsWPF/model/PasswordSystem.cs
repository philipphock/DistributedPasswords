using DistributedPasswordsWPF.model.dataobjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedPasswordsWPF.model
{
    class PasswordSystem
    {
        private Header header;

        public void Init()
        {
            DEBUG.enabled = true;
            Settings.Init();
            
            header = new Header();

            
            string db = Settings.DB_PATH;
            string keys = Settings.KEYS_PATH;

            if (string.IsNullOrEmpty(db) || string.IsNullOrEmpty(keys))
            {
                Router.instance.DisplayPage(Router.Pages.PathSettings);
            }
            else
            {
                Router.instance.DisplayPage(Router.Pages.Unlock);
            }

            

            
            

        }

        public bool IsHeaderFilePresent()
        {
            return this.header.IsHeaderFilePresent();
        }


        public bool Unlock(string password)
        {
            bool success = this.header.DecryptHeader(password);

            return success;
        }
        public void CreateHeader(string password)
        {
            this.header.CreateHeader(password);
        }

        public List<PasswordEntry> ReadDatabase()
        {

            string[] ss = FileHelper.ListFiles(Settings.DB_PATH);
            List<PasswordEntry> ret = new List<PasswordEntry>(ss.Length);
            int cnt = 0;
            foreach (string s in ss)
            {
                
                string dec = this.header.DecryptWithHeaderPassword(s);
                string enc = s;

                PasswordEntry e = new PasswordEntry
                {
                    Id = dec,
                    EncryptedFileName = enc
                };

                ret.Add(e);

                cnt++;
            }

            return ret;
        }

        public static PasswordSystem Instance = new PasswordSystem();
        private PasswordSystem()
        {
           

        }
    }
}
