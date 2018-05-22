using DistributedPasswordsWPF.model.dataobjects;
using Newtonsoft.Json;
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

            dynamic o = JsonConvert.DeserializeObject(decryptedFileContent);
            foreach (dynamic d in o.usernames)
            {
                Username u = new Username
                {
                    Name = d.username,
                    Password = d.password,
                    Email = d.email,
                    Notes = d.notes
                };

                entry.Add(u);

            }
            
        }
    }
}
