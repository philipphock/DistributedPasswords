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

        private static string DB_DIR = "";
        private static string KEYS_DIR = "";


        static Settings()
        {
            
        }

        public static void Init()
        {
            bool init = false;
            if (!System.IO.Directory.Exists(SETTINGS_DIR))
            {
                init = true;
                System.IO.Directory.CreateDirectory(SETTINGS_DIR);
            }
            
            SQLiteConnection.CreateFile(SETTINGS);
            SQLiteConnection dbConnection = new SQLiteConnection("Data Source = " + SETTINGS + "; Version = 3;");
            dbConnection.Open();

            if (init)
            {

                string sql = @"
                    CREATE TABLE `settings` (
	                    `ID`	INTEGER NOT NULL UNIQUE,
	                    `KEY`	TEXT NOT NULL,
	                    `VALUE`	TEXT,
	                    PRIMARY KEY(`ID`)
                    );
                ";
                
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                command.ExecuteNonQuery();

                string sql2 = @"
                    #INSERT INTO `settings`(`ID`,`KEY`,`VALUE`) VALUES (1,'',NULL);
                ";

                SQLiteCommand command2 = new SQLiteCommand(sql2, dbConnection);
                command2.ExecuteNonQuery();

            }

            
            Debug.WriteLine("init");
        }


        public static string KEYS_PATH
        {
            get
            {
                return KEYS_DIR;
            }

            set
            {
                KEYS_DIR = value;
            }
        }

        public static string DB_PATH
        {
            get
            {
                return DB_DIR;
            }

            set
            {
                DB_DIR = value;
            }
        }
    }
}
