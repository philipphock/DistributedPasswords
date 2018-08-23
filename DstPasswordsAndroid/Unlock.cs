using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace DstPasswordsAndroid
{
    [Activity(Label = "Unlock")]
    public class Unlock : Activity
    {
        private readonly string KEY_DB = "DB";
        private readonly string KEY_KEYS = "KEYS";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Unlock);
            _loadValues();
            // Create your application here
        }

        protected override void OnPause()
        {
            base.OnPause();
            _storeValues();
        }

        private void _loadValues()
        {
            EditText keysUIField = FindViewById<EditText>(Resource.Id.keys);
            EditText dbUIField = FindViewById<EditText>(Resource.Id.db);

            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
            var keys = prefs.GetString(KEY_KEYS,"");
            var db = prefs.GetString(KEY_DB, "");

            keysUIField.Text = keys;
            dbUIField.Text = db;
        }

        private void _storeValues()
        {
            EditText keysUIField = FindViewById<EditText>(Resource.Id.keys);
            EditText dbUIField = FindViewById<EditText>(Resource.Id.db);

            var keys = keysUIField.Text;
            var db = dbUIField.Text;
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutString(KEY_DB, db);
            editor.PutString(KEY_KEYS, keys);
            
            editor.Apply();        // applies changes asynchronously on newer APIs
            

        }

        public void UnlockBtn(View v)
        {
            
        }

    }
}
