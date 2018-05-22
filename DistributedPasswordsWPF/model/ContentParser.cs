using DistributedPasswordsWPF.debug;
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

            DEBUG.Print(decryptedFileContent);
            dynamic o = JsonConvert.DeserializeObject(decryptedFileContent);
            
            if (o is null)
            {
                // TODO handle corrupt database
                DEBUG.Print("ContentParser", "Error reading json file");
            }
            
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

        public string GetJSONString(PasswordEntry entry)
        {
            return JsonConvert.SerializeObject(entry);
        }
    }
}
