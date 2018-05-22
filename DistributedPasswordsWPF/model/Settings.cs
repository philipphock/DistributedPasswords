using DistributedPasswordsWPF.debug;
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
            UPDATE `settings` SET `VALUE`=:v WHERE _rowid_='1';
        ";

        private static readonly string UPDATE_KEYS = @"
            UPDATE `settings` SET `VALUE`=:v WHERE _rowid_='2';
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

        private static bool _initCalled = false;

        public static void Init()
        {
            if (_initCalled)
            {
                //throw new InvalidOperationException("Settings.Init() already called");
                return;
            }

            _initCalled = true;


            bool init = false;
            if (!System.IO.Directory.Exists(SETTINGS_DIR) || !System.IO.File.Exists(SETTINGS))
            {
                init = true;
                System.IO.Directory.CreateDirectory(SETTINGS_DIR);
                SQLiteConnection.CreateFile(SETTINGS);

            }

            dbConnection = new SQLiteConnection("Data Source = " + SETTINGS + "; Version = 3;");
            dbConnection.Open();

            if (init)
            {
                DEBUG.Print("Settings", "init");


                SQLiteCommand command = new SQLiteCommand(CREATE_TABLE, dbConnection);
                SQLiteCommand command2 = new SQLiteCommand(CREATE_KV_ENTRIES, dbConnection);
                command.ExecuteNonQuery();
                command2.ExecuteNonQuery();

            }
            


        }

        


        public static string KEYS_PATH
        {
            get
            {
                if (!_initCalled)
                {
                    throw new InvalidOperationException("Init not yet called");
                }
                if (KEYS_DIR == "" || KEYS_DIR == null)
                {

                    SQLiteCommand command2 = new SQLiteCommand(SELECT_KEYS, dbConnection);

                    SQLiteDataReader reader2 = command2.ExecuteReader();
                    while (reader2.Read())
                    {
                        KEYS_DIR = reader2["VALUE"].ToString();
                    }
                }

                return KEYS_DIR;
            }

            set
            {
                SQLiteCommand command = new SQLiteCommand(UPDATE_KEYS, dbConnection);
                command.Parameters.Add("v", System.Data.DbType.String).Value = value;
                command.ExecuteNonQuery();
                KEYS_DIR = value;

            }
        }

        public static string DB_PATH
        {
            get
            {
                if (!_initCalled)
                {
                    throw new InvalidOperationException("Init not yet called");
                }
                if (DB_DIR == "" || DB_DIR == null)
                {
                    SQLiteCommand command = new SQLiteCommand(SELECT_DB, dbConnection);
                    SQLiteDataReader reader1 = command.ExecuteReader();
                    while (reader1.Read())
                    {
                        DB_DIR = reader1["VALUE"].ToString();
                    }
                }
                return DB_DIR;
            }

            set
            {
                SQLiteCommand command = new SQLiteCommand(UPDATE_DB, dbConnection);
                command.Parameters.Add("v", System.Data.DbType.String).Value = value;
                command.ExecuteNonQuery();

                DB_DIR = value;
            }
        }


    }
}
