using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedPasswordsWPF.model.dataobjects
{
    public class PasswordEntry: INotifyPropertyChanged
    {
        private string _id;
        private string _encryptedfilename;
        private List<Username> usernames = new List<Username>();

        
        public string Encryptedfilename { get => _encryptedfilename; set => _encryptedfilename = value; }
        
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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
