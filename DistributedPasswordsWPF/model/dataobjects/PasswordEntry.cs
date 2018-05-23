using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedPasswordsWPF.model.dataobjects
{
    public class PasswordEntry: INotifyPropertyChanged, IDeepCloneable<PasswordEntry>
    {
        private string _id;

        private string _encryptedfilename;
        private List<Username> usernames = new List<Username>();

        [JsonIgnore]
        public string Encryptedfilename { get => _encryptedfilename; set => _encryptedfilename = value; }
        
        [JsonIgnore]
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                Notify("Id");
            }
        }

        public void Add(Username n)
        {
            usernames.Add(n);
            Notify("Usernames");
        }

        public void Remove(Username username)
        {
            usernames.Remove(username);
        }

        [JsonProperty(PropertyName = "usernames")]
        public IList<Username> Usernames
        {
            get
            {
                return usernames.AsReadOnly();
            }
        }

        private void Notify(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }

        public void NotifyObservers(bool notifyUsernames = false)
        {
            Notify("Usernames");
            Notify("Id");
            if (notifyUsernames)
            {
                foreach (Username n in Usernames)
                {
                    n.NotifyObservers();
                }
            }
            

        }

        

        public PasswordEntry DeepClone()
        {
            var ret = new PasswordEntry
            {
                _id = _id,
                _encryptedfilename = _encryptedfilename
            };
            foreach (var u in usernames)
            {
                ret.Add(u.DeepClone());
            }

            return ret;

        }

        public override string ToString()
        {
            string usernames = "";
            foreach (Username u in Usernames)
            {
                usernames += u.ToString()+"\n";
            }
            return String.Format(
@"ENTRY: {0}
EncryptedFileName: {1}
Usernames:
{2}
----------------------", Id, Encryptedfilename, usernames);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
