using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedPasswordsWPF.model
{
    class Settings
    {
        private static string SETTINGS = "assets/db/settings.sqlite";
        private static string SETTINGS_DIR = "assets/db/";

        static Settings()
        {
            
        }

        public static void Init()
        {
            if (!System.IO.Directory.Exists(SETTINGS_DIR))
            {
                System.IO.Directory.CreateDirectory(SETTINGS_DIR);
            }
            
            SQLiteConnection.CreateFile(SETTINGS);
            SQLiteConnection dbConnection = new SQLiteConnection("Data Source = " + SETTINGS + "; Version = 3;");
            dbConnection.Open();
            Debug.WriteLine("init");
        }
    }
}
