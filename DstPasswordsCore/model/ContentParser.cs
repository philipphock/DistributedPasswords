using DistributedPasswordsWPF.debug;
using DistributedPasswordsWPF.model.dataobjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedPasswordsWPF.model
{
    public static class ContentParser
    {
        public static void ParseToEntry(PasswordEntry entry, string decryptedFileContent)
        {

            //Debug.WriteLine(decryptedFileContent);
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
                    Notes = d.notes,
                    TFA = d.TFA
                };

                entry.Add(u);

            }

            
            
        }

        public static string GetJSONString(PasswordEntry entry)
        {
            return JsonConvert.SerializeObject(entry);
        }
    }
}
