
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using DstPasswordsCore.model;
using DstPasswordsAndroid.model;
using Android.Content;

namespace DstPasswordsAndroid
{
    [Activity(Label = "Unlock")]
    public class Unlock : Activity
    {

        private EditText keysUIField;
        private EditText dbUIField;
        private EditText passwordUIFiled;
        private ISettings _settings;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Unlock);
            keysUIField = FindViewById<EditText>(Resource.Id.keys);
            dbUIField = FindViewById<EditText>(Resource.Id.db);
            passwordUIFiled = FindViewById<EditText>(Resource.Id.password);

            _settings = new Settings();

            keysUIField.Text = _settings.KEYS_PATH;
            dbUIField.Text = _settings.DB_PATH;
            // Create your application here
        }

        protected override void OnPause()
        {
            base.OnPause();
            _settings.KEYS_PATH = keysUIField.Text;
            _settings.DB_PATH = dbUIField.Text;
        }

        

        public async void UnlockBtn(View v)
        {
            PasswordSystem.Instance.Init(_settings);
            passwordUIFiled.Enabled = false;
            bool unlocked = await PasswordSystem.Instance.Unlock(passwordUIFiled.Text);
            passwordUIFiled.Enabled = true;
            if (unlocked)
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            }

        }

    }
}
