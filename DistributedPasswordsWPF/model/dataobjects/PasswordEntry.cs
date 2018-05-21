using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedPasswordsWPF.model.dataobjects
{
    class PasswordEntry
    {
        private string _id;
        private string _encryptedfilename;

        

        public string EncryptedFileName
        {
            get
            {
                return _encryptedfilename;
            }
            set
            {
                _encryptedfilename = value;
            }
        }

        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

    }
}
