using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedPasswordsWPF.model.dataobjects
{
    public class EncryptedEntry
    {
        private string _text;
        private string _filename;
        private string _id;

        public EncryptedEntry(string id, string text, string filename)
        {
            this._text = text;
            this._filename = filename;
            this._id = id;
        }

        

        public string Id
        {
            get
            {
                return _id;
            }
        }

        public string Filename { get => _filename;}

        public void Save()
        {
            File.WriteAllText(Path.Combine(Settings.DB_PATH, Filename), _text);
        }


        public static EncryptedEntry FromDecrypted(PasswordEntry e)
        {
            string s = ContentParser.GetJSONString(e);
            string t = PasswordSystem.Instance.Encrypt(s);
            string f = PasswordSystem.Instance.Encrypt(e.Id);
            EncryptedEntry ret = new EncryptedEntry(e.Id, t, f);

            return ret;
        }


        public PasswordEntry Decrypt
        {
            get
            {
                PasswordEntry ret = new PasswordEntry(_id);
                string decryptedContent = PasswordSystem.Instance.Decrypt(_text);
                ContentParser.ParseToEntry(ret, decryptedContent);
                return ret;
            }
        }

        public void Update(PasswordEntry e)
        {
            string todelete = null;

            if (_id != e.Id)
            {
                //id has changed, so must our filename
                todelete = Filename;
                _filename = PasswordSystem.Instance.Encrypt(e.Id);
                this._id = e.Id;
            }

            string s = ContentParser.GetJSONString(e);
            _text = PasswordSystem.Instance.Encrypt(s);

            File.WriteAllText(Path.Combine(Settings.DB_PATH, Filename), _text);

            if (todelete != null)
            {
                File.Delete(Path.Combine(Settings.DB_PATH, todelete));
            }

        }
    }
}
