using DstPasswordsCore.model.dataobjects;
using Newtonsoft.Json;
using System.Diagnostics;

namespace DstPasswordsCore.model
{
    public static class ContentParser
    {
        class Intermediate1
        {
            public Intermediate2[] usernames;
        }
        class Intermediate2
        {
            public string username;
            public string password;
            public string email;
            public string notes;
            public string TFA;
        }
        public static void ParseToEntry(PasswordEntry entry, string decryptedFileContent)
        {
            int last = decryptedFileContent.LastIndexOf('}')+1;
            
            if (last != decryptedFileContent.Length)
            {

                decryptedFileContent=decryptedFileContent.Substring(0, last);
                Debug.WriteLine("corrupt data recognized");

            }


            //Debug.WriteLine(decryptedFileContent);
            Intermediate1 o = JsonConvert.DeserializeObject<Intermediate1>(decryptedFileContent);
            
            
            
            foreach (Intermediate2 d in o.usernames)
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
