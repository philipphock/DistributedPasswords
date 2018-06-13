using DistributedPasswordsWPF.debug;
using DistributedPasswordsWPF.model.dataobjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DistributedPasswordsWPF.model
{
    public class PasswordSystem
    {
        private Header header;
        private List<EncryptedEntry> _cachedList;


        public bool Init()
        {
            Settings.Init();
            
            header = new Header();

            
            string db = Settings.DB_PATH;
            string keys = Settings.KEYS_PATH;

            if (string.IsNullOrEmpty(db) || string.IsNullOrEmpty(keys))
            {
                return true;
            }
            else
            {
                return false;
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

        public string Encrypt(string plaintext)
        {
            return header.EncryptWithHeaderPassword(plaintext);
        }

        public string Decrypt(string text)
        {
            return header.DecryptWithHeaderPassword(text);
        }
        public void CreateHeader(string password)
        {
            this.header.CreateHeader(password);
        }

        public List<EncryptedEntry> Filter(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return _cachedList;
            }
            var ret = _cachedList.Where(i => i.Id.Contains(s));            
            return ret.ToList();
        }
        

        public List<EncryptedEntry> ReadDatabase()
        {

            string[] ss = FileHelper.ListFiles(Settings.DB_PATH);
            List<EncryptedEntry> ret = new List<EncryptedEntry>(ss.Length);
            int cnt = 0;
            foreach (string s in ss)
            {
                
                string enc = s;
                string id = Decrypt(enc);
                string encryptedContent = File.ReadAllText(Path.Combine(Settings.DB_PATH, enc));
                EncryptedEntry e = new EncryptedEntry(id, encryptedContent, enc);

                string decryptedContent = this.header.DecryptWithHeaderPassword(encryptedContent);
                
                ret.Add(e);

                cnt++;
            }
            _cachedList = ret;
            SelectedEntry = null;
            return ret;
        }



       

        public static PasswordSystem Instance = new PasswordSystem();
        
        public void Lock()
        {
            SelectedEntry = null;
            _cachedList = null;
            header.Clear();
        }

        public bool ChangePassword(string old, string newp)
        {
            if (header.DecryptHeader(old, true))
            {
                header.ChangePassword(newp);
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public EncryptedEntry SelectedEntry;
        public void Select(EncryptedEntry e)
        {
            SelectedEntry = e;
        }
        public bool TrySelect(string id)
        {
            if (_cachedList == null) return false;

            if (SelectedEntry != null && SelectedEntry.Id == id)
            {
                return true;
            }

            foreach (EncryptedEntry e in _cachedList)
            {
                if (e.Id == id)
                {
                    SelectedEntry = e;
                    return true;
                }
            }
            
            return false;
        }

        public string GetPasswordFromSelection()
        {
            if (SelectedEntry == null) return null;
            PasswordEntry e = SelectedEntry.Decrypt;
            if (e.Usernames.Count == 0) return null;

            return e.Usernames[0].Password;
        }

        public string GetUsernameFromSelection()
        {
            if (SelectedEntry == null) return null;
            PasswordEntry e = SelectedEntry.Decrypt;

            if (e.Usernames.Count == 0) return null;

            return e.Usernames[0].Name;
        }

        public event EventHandler<Locked> LockedHandler;
        

        private PasswordSystem()
        {
           

        }
    }
    public class Locked : EventArgs
    {
        
    }

}
