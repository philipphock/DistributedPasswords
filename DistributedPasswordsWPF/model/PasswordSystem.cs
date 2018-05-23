using DistributedPasswordsWPF.debug;
using DistributedPasswordsWPF.model.dataobjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedPasswordsWPF.model
{
    class PasswordSystem
    {
        private Header header;
        private List<PasswordEntry> _cachedList;


        public void Init()
        {
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

        public List<PasswordEntry> Filter(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return _cachedList;
            }
            List<PasswordEntry> ret = new List<PasswordEntry>();
            foreach (PasswordEntry e in _cachedList)
            {
                if (string.IsNullOrEmpty(e.Id)) continue;
                if (e.Id.Contains(s))
                {
                    ret.Add(e);
                }
            }
            return ret;
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
                    Encryptedfilename = enc
                };

                string encryptedContent = File.ReadAllText(Path.Combine(Settings.DB_PATH, enc));
                string decryptedContent = this.header.DecryptWithHeaderPassword(encryptedContent);
                
                ContentParser.ParseToEntry(e, decryptedContent);
                
                ret.Add(e);

                cnt++;
            }
            _cachedList = ret;
            SelectedEntry = null;
            return ret;
        }


        public ContentParser ContentParser { get; } = new ContentParser();

        public void Save(PasswordEntry entry)
        {
            

            string todelete = null;

            string s = ContentParser.GetJSONString(entry);
            string es = header.EncryptWithHeaderPassword(s);
            if (string.IsNullOrWhiteSpace(entry.Encryptedfilename))
            {
                entry.Encryptedfilename = header.EncryptWithHeaderPassword(entry.Id);
            }
            else
            {
                //handle renamed id, here we must change the encryptedfilename
                if (header.DecryptWithHeaderPassword(entry.Encryptedfilename) != entry.Id)
                {
                    todelete = entry.Encryptedfilename;
                    //id has changed, so must our encryptedfilename
                    entry.Encryptedfilename = header.EncryptWithHeaderPassword(entry.Id);


                }
            }
            
            File.WriteAllText(Path.Combine(Settings.DB_PATH, entry.Encryptedfilename), es);
            //then we get rid of the old file

            if (todelete != null)
            {
                File.Delete(Path.Combine(Settings.DB_PATH, todelete));
            }

        }

        public static PasswordSystem Instance = new PasswordSystem();
        
        public void Lock()
        {
            SelectedEntry = null;
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

        public PasswordEntry SelectedEntry;
        public void Select(PasswordEntry e)
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

            foreach (PasswordEntry e in _cachedList)
            {
                DEBUG.Print(GetType(),"entry:", e.Id,"param", id);
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
            if (SelectedEntry.Usernames.Count == 0) return null;

            return SelectedEntry.Usernames[0].Password;
        }

        public string GetUsernameFromSelection()
        {
            if (SelectedEntry == null) return null;
            if (SelectedEntry.Usernames.Count == 0) return null;

            return SelectedEntry.Usernames[0].Name;
        }

        private PasswordSystem()
        {
           

        }
    }
}
