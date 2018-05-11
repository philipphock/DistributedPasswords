using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedPasswordsWPF.model
{
    class PasswordSystem
    {
        public PasswordSystem()
        {
            //FileHelper.ListDatabaseFiles();
            Settings.Init();
            try
            {
                string db = Settings.DB_PATH;
                string keys = Settings.KEYS_PATH;
                Router.instance.DisplayPage(Router.Pages.Unlock);

            }
            catch (ArgumentException e)
            {
                Router.instance.DisplayPage(Router.Pages.PathSettings);

            }



        }
    }
}
