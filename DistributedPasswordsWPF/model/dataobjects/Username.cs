using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedPasswordsWPF.model.dataobjects
{
    public class Username: INotifyPropertyChanged, IDeepCloneable<Username>
    {
        private string username;
        private string email;
        private string password;
        private string notes;

        public string Name {
            get{
               return username;
            }
            set{
                username = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
            }
        }
        public string Email { get => email; set {
                email = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Email"));
            }
        }
        public string Password { get => password; set
            {
                password = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Password"));

            }
        }
        public string Notes { get => notes; set
            {
                notes = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Notes"));

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Username DeepClone()
        {
            return new Username
            {
                Name = Name,
                Email = Email,
                Password = Password,
                Notes = Notes
            };

            
        }

        public void NotifyObservers()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Password"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Email"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Notes"));

        }

        public override string ToString()
        {
            return String.Format(@"
username: {0}
email: {1}
password: {2}
notes: {3}
",Name, Email, Password, Notes);
        }
    }
}
