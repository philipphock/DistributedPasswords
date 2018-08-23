using Android.App;
using Android.Content;
using Android.Preferences;
using DstPasswordsCore.model;
using System;

namespace DstPasswordsAndroid.model
{
    public class Settings : ISettings
    {
        private readonly string KEY_DB = "DB";
        private readonly string KEY_KEYS = "KEYS";


        public string DB_PATH
        {
            get
            {
                ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
                var db = prefs.GetString(KEY_DB, "");
                return db;
            }
            set
            {
                ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
                ISharedPreferencesEditor editor = prefs.Edit();
                editor.PutString(KEY_DB, value);

                editor.Apply();       
            }

        }
        public string KEYS_PATH
        {
            get
            {
                ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
                var keys = prefs.GetString(KEY_KEYS, "");

                return keys;
            }

            set
            {
                ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
                ISharedPreferencesEditor editor = prefs.Edit();
                editor.PutString(KEY_KEYS, value);

                editor.Apply();
            }
        }

        public void Init()
        {
            
        }

       

        private void _storeValues()
        {

            
            


        }
    }
}