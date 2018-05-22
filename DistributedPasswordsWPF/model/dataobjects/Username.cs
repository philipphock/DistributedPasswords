using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedPasswordsWPF.model.dataobjects
{
    public class Username
    {
        private string username;
        private string email;
        private string password;
        private string notes;

        public string Name { get => username; set => username = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string Notes { get => notes; set => notes = value; }

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
