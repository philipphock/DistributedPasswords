using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedPasswordsWPF.model
{
    class PasswordSystem
    {
        private readonly Header header = new Header();

        public void Init()
        {
            Settings.Init();

            
            

            //Debug.WriteLine(_header);

            try
            {
                string db = Settings.DB_PATH;
                string keys = Settings.KEYS_PATH;
                Router.instance.DisplayPage(Router.Pages.Unlock);

            }
            catch (ArgumentException)
            {
                Router.instance.DisplayPage(Router.Pages.PathSettings);

            }
        }

        public bool IsHeaderFilePresent()
        {
            return this.header.IsHeaderFilePresent();
        }


        public bool Unlock()
        {
            return false;
        }

        public static PasswordSystem Instance = new PasswordSystem();
        private PasswordSystem()
        {
            //FileHelper.ListDatabaseFiles();
            


        }
    }
}
