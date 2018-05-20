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

        private async void Init()
        {
            string header = await this.header.GenerateHeader();
            Debug.WriteLine(header);
        }

        public async void EncryptHeader()
        {

        }

        public async void DecryptHeader()
        {

        }

        public PasswordSystem()
        {
            //FileHelper.ListDatabaseFiles();
            //Settings.Init();
            Init();

            try
            {
                string db = Settings.DB_PATH;
                string keys = Settings.KEYS_PATH;
                //Router.instance.DisplayPage(Router.Pages.Unlock);

            }
            catch (ArgumentException)
            {
                Router.instance.DisplayPage(Router.Pages.PathSettings);

            }



        }
    }
}
