using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedPasswordsWPF.model.dataobjects
{
    public class PasswordEntry
    {
        private string _id;
        private string _encryptedfilename;
        private List<Username> usernames = new List<Username>();

        
        public string Encryptedfilename { get => _encryptedfilename; set => _encryptedfilename = value; }
        
        public string Id { get => _id; set => _id = value; }
        public List<Username> Usernames { get => usernames; set => usernames = value; }
    }
}
