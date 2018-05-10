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
        private static string SETTINGS_DIR = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "DstPassword");
        private static string SETTINGS = System.IO.Path.Combine(SETTINGS_DIR,"settings.sqlite");

        private static SQLiteConnection dbConnection;

        private static string DB_DIR = null;
        private static string KEYS_DIR = null;


        private static readonly string CREATE_TABLE = @"
                    CREATE TABLE `settings` (
	                    `ID`	INTEGER NOT NULL UNIQUE,
	                    `KEY`	TEXT NOT NULL,
	                    `VALUE`	TEXT,
	                    PRIMARY KEY(`ID`)
                    );
                ";

        private static readonly string CREATE_KV_ENTRIES = @"
                    INSERT INTO `settings`(`ID`,`KEY`,`VALUE`) VALUES (1,'DB',NULL);
                    INSERT INTO `settings`(`ID`,`KEY`,`VALUE`) VALUES (2,'KEYS',NULL);
                ";

        private static readonly string UPDATE_DB = @"
            UPDATE `settings` SET `VALUE`=? WHERE _rowid_='1';
        ";

        private static readonly string UPDATE_KEYS = @"
            UPDATE `settings` SET `VALUE`=? WHERE _rowid_='2';
        ";

        private static readonly string SELECT_DB = @"
            SELECT `VALUE` FROM `settings` WHERE `ID` = 1;        
        ";

        private static readonly string SELECT_KEYS = @"
            SELECT `VALUE` FROM `settings` WHERE `ID` = 2;        
        ";


        static Settings()
        {
            
        }

        public static void Init()
        {
            bool init = false;
            Debug.WriteLine(SETTINGS);
            if (!System.IO.Directory.Exists(SETTINGS_DIR))
            {
                init = true;
                System.IO.Directory.CreateDirectory(SETTINGS_DIR);
                SQLiteConnection.CreateFile(SETTINGS);

            }

            dbConnection = new SQLiteConnection("Data Source = " + SETTINGS + "; Version = 3;");
            dbConnection.Open();

            if (init)
            {

                Debug.WriteLine("create table");

                SQLiteCommand command = new SQLiteCommand(CREATE_TABLE, dbConnection);
                SQLiteCommand command2 = new SQLiteCommand(CREATE_KV_ENTRIES, dbConnection);
                command.ExecuteNonQuery();
                command2.ExecuteNonQuery();

            }
            else
            {
                SQLiteCommand command = new SQLiteCommand(SELECT_DB, dbConnection);
                SQLiteCommand command2 = new SQLiteCommand(SELECT_KEYS, dbConnection);
                SQLiteDataReader reader1 = command.ExecuteReader();
                while (reader1.Read())
                {
                    DB_PATH = reader1["VALUE"].ToString();
                }
                SQLiteDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    KEYS_PATH = reader2["VALUE"].ToString();
                }
            }


        }

        


        public static string KEYS_PATH
        {
            get
            {
                if (KEYS_PATH == "" || KEYS_PATH == null)
                {
                    throw new System.ArgumentException("Path not set");
                }

                return KEYS_DIR;
            }

            set
            {
                SQLiteCommand command = new SQLiteCommand(UPDATE_KEYS, dbConnection);
                command.Parameters.Add(value, System.Data.DbType.String);
                command.ExecuteNonQuery();
                KEYS_DIR = value;

            }
        }

        public static string DB_PATH
        {
            get
            {
                if (DB_DIR == "" || DB_DIR == null)
                {
                    throw new System.ArgumentException("Path not set");
                }
                return DB_DIR;
            }

            set
            {
                SQLiteCommand command = new SQLiteCommand(UPDATE_DB, dbConnection);
                command.Parameters.Add(value, System.Data.DbType.String);
                command.ExecuteNonQuery();

                DB_DIR = value;
            }
        }


    }
}
