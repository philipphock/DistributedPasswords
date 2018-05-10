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
                ViewModel.instance.DisplayPage(ViewModel.Pages.Unlock);

            }
            catch (ArgumentException e)
            {
                ViewModel.instance.DisplayPage(ViewModel.Pages.PathSettings);

            }



        }
    }
}
