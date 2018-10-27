using DstPasswordsCore.model;
using System;
using System.Data.SQLite;
using System.Diagnostics;

namespace DistributedPasswordsWPF.model
{
    public class Settings : ISettings
    {
        private readonly bool DEBUGPATH = false;
        private readonly string SETTINGS_DIR;
        private readonly string SETTINGS;

        private SQLiteConnection dbConnection;

        private string DB_DIR = null;
        private string KEYS_DIR = null;
        private bool? PW_CPY = null;


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
                    INSERT INTO `settings`(`ID`,`KEY`,`VALUE`) VALUES (3,'PW_CPY',NULL);
                ";

        private static readonly string UPDATE_DB = @"
            UPDATE `settings` SET `VALUE`=:v WHERE _rowid_='1';
        ";

        private static readonly string UPDATE_KEYS = @"
            UPDATE `settings` SET `VALUE`=:v WHERE _rowid_='2';
        ";

        private static readonly string UPDATE_PW_CPY = @"
            UPDATE `settings` SET `VALUE`=:v WHERE _rowid_='3';
        ";

        private static readonly string SELECT_DB = @"
            SELECT `VALUE` FROM `settings` WHERE `ID` = 1;        
        ";

        private static readonly string SELECT_KEYS = @"
            SELECT `VALUE` FROM `settings` WHERE `ID` = 2;        
        ";

        private static readonly string SELECT_PWCPY = @"
            SELECT `VALUE` FROM `settings` WHERE `ID` = 3;        
        ";

        private static readonly string CREATE_ROW3 = "INSERT INTO `settings`(`ID`,`KEY`,`VALUE`) VALUES (3,'PW_CPY',NULL);";

        public Settings()
        {
            SETTINGS_DIR = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "DstPassword");
            SETTINGS = System.IO.Path.Combine(SETTINGS_DIR, "settings.sqlite");

            Init();
        }

        private static bool _initCalled = false;

        public void Init()
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


                SQLiteCommand command = new SQLiteCommand(CREATE_TABLE, dbConnection);
                SQLiteCommand command2 = new SQLiteCommand(CREATE_KV_ENTRIES, dbConnection);
                command.ExecuteNonQuery();
                command2.ExecuteNonQuery();

            }

            // check version
            SQLiteCommand command3 = new SQLiteCommand(SELECT_PWCPY, dbConnection);
            SQLiteDataReader reader1 = command3.ExecuteReader();

            if (!reader1.HasRows)
            {
                // row 3 not present, means old db version
                new SQLiteCommand(CREATE_ROW3, dbConnection).ExecuteNonQuery();

            }

        }

        


        public string KEYS_PATH
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
                if (DEBUGPATH)
                {
                    return @"D:\assets\keys";
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

        public string DB_PATH
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
                if (DEBUGPATH)
                {
                    return @"D:\assets\db";
                }
                //return @"D:\assets\db";
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


        public bool CPY_PW
        {
            get
            {
                if (!_initCalled)
                {
                    throw new InvalidOperationException("Init not yet called");
                }
                if (PW_CPY == null)
                {
                    SQLiteCommand command = new SQLiteCommand(SELECT_PWCPY, dbConnection);
                    SQLiteDataReader reader1 = command.ExecuteReader();
                    while (reader1.Read())
                    {
                        PW_CPY = reader1["VALUE"].ToString() == "1";
                    }
                }
                
                if (PW_CPY == null)
                {
                    PW_CPY = false;
                }
                
                return PW_CPY ==  true;
            }

            set
            {
                string _value = "0";
                if (value)
                {
                    _value = "1";
                }
                SQLiteCommand command = new SQLiteCommand(UPDATE_PW_CPY, dbConnection);
                command.Parameters.Add("v", System.Data.DbType.String).Value = _value;
                command.ExecuteNonQuery();

                PW_CPY = value;
            }
        }

    }
}
