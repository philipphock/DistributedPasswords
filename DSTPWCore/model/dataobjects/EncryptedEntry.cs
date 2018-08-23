using System.IO;

namespace DstPasswordsCore.model.dataobjects
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

        public bool Save()
        {
            var Filename = this.Filename;
            
            if (!_checkFile(Filename, false, false))
            {
                return false;
            }
            File.WriteAllText(Path.Combine(PasswordSystem.Instance.Settings.DB_PATH, Filename), _text);
            return true;
        }


        public static EncryptedEntry FromDecrypted(PasswordEntry e)
        {
            string s = ContentParser.GetJSONString(e);
            string t = PasswordSystem.Instance.Encrypt(s);
            string f = PasswordSystem.Instance.Encrypt(e.Id);
            EncryptedEntry ret = new EncryptedEntry(e.Id, t, f);

            return ret;
        }

        public string DecryptAsString()
        {
            return PasswordSystem.Instance.Decrypt(_text);
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
            bool idChanged = _id != e.Id;
            do
            {
                if (idChanged)
                {
            
                    //id has changed, so must our filename
                    todelete = Filename;
                    _filename = PasswordSystem.Instance.Encrypt(e.Id);
                    this._id = e.Id;
                }

                string s = ContentParser.GetJSONString(e);
                _text = PasswordSystem.Instance.Encrypt(s);

            } while (!_checkFile(_filename, true, idChanged));

            File.WriteAllText(Path.Combine(PasswordSystem.Instance.Settings.DB_PATH, Filename), _text);

            if (todelete != null)
            {
                File.Delete(Path.Combine(PasswordSystem.Instance.Settings.DB_PATH, todelete));
            }

        }
        
       
        private bool _checkFile(string filename, bool isUpdate, bool idsChanged)
        {
            
            string fqfn = Path.Combine(PasswordSystem.Instance.Settings.DB_PATH, filename);
            if (!File.Exists(fqfn))
            {
                return true;
            }



            /*
             when the program continues to the code below, this means the file exists.. 
             this might be no problem but it might override an existing entry, there are the following cases:

            case A: isUpdate == false
                1) the file we override is another entry, me must change the new filename until there is no other value
                
            case B: isUpdate == true:
                a) we do not update the id: 
                   that's our file, go ahead and override it
                   
                b) we update the id:
                   the file we override is another entry, me must change the new filename until there is no other value
            
            */
            

            if (isUpdate)
            {
                //0
                if (!idsChanged)
                {
                    return true;
                }
                else
                {
                    return false;
                }

                //
            }
            else
            {
                return false;
            }
        }
        
    }
}
