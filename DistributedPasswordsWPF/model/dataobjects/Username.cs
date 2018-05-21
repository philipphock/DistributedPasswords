using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedPasswordsWPF.model.dataobjects
{
    class Username
    {
        public string username;
        public string email;
        public string password;
        public string notes;

        public override string ToString()
        {
            return String.Format(@"
username: {0}
email: {1}
password: {2}
notes: {3}
",username, email, password, notes);
        }
    }
}
