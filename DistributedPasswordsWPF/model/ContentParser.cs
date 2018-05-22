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

            DEBUG.Print("ContentParser", decryptedFileContent);

            dynamic o = JsonConvert.DeserializeObject(decryptedFileContent);
            DEBUG.Print("ContentParser", o);
            
            if (o is null)
            {
                // TODO handle corrupt database
                DEBUG.Print("ContentParser", "Error reading json file");
            }
            try
            {
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
            catch (NullReferenceException)
            {
                entry.Id = o.Id;
                foreach (dynamic d in o.Usernames)
                {
                    Username u = new Username
                    {
                        Name = d.Name,
                        Password = d.Password,
                        Email = d.Email,
                        Notes = d.Notes
                    };

                    entry.Add(u);

                }
            }
        }

        public string GetJSONString(PasswordEntry entry)
        {
            return JsonConvert.SerializeObject(entry);
        }
    }
}
