using DistributedPasswordsWPF.model.dataobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedPasswordsWPF.model
{
    class ContentParser
    {
        public void ParseToEntry(PasswordEntry entry, string decryptedFileContent)
        {
            DEBUG.Print(this.GetType(), decryptedFileContent);
        }
    }
}
