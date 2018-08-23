
using Android.App;
using Android.OS;
using Android.Widget;
using DstPasswordsCore.model;
using DstPasswordsAndroid.model;
using Android.Content;
using Android;
using System.Linq;
using Android.Content.PM;

namespace DstPasswordsAndroid
{
    [Activity(Label = "Unlock")]
    public class Unlock : Activity
    {

        private EditText keysUIField;
        private EditText dbUIField;
        private EditText passwordUIFiled;
        private Button unlockButton;
        private ISettings _settings;

        private const int EVENT_PERMISSIONREQEST = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Unlock);
            keysUIField = FindViewById<EditText>(Resource.Id.keys);
            dbUIField = FindViewById<EditText>(Resource.Id.db);
            passwordUIFiled = FindViewById<EditText>(Resource.Id.password);
            unlockButton = FindViewById<Button>(Resource.Id.unlock);

            unlockButton.Click += (o, e) =>
            {
                _btnAsync();
            };
            _settings = new Settings();

            keysUIField.Text = _settings.KEYS_PATH;
            dbUIField.Text = _settings.DB_PATH;

            if (Application.Context.CheckSelfPermission(Manifest.Permission.ReadExternalStorage) == Android.Content.PM.Permission.Denied)
            {
                RequestPermissions(new string[] { Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage }, EVENT_PERMISSIONREQEST);
            }
            

    

            // Create your application here
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            if (requestCode == EVENT_PERMISSIONREQEST)
            {
                if (grantResults.Contains(Permission.Denied)){
                    Toast.MakeText(Application.Context, "You must grant all permissions!",ToastLength.Short).Show();
                    RequestPermissions(new string[] { Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage }, EVENT_PERMISSIONREQEST);
                }
                else
                {
                    Toast.MakeText(Application.Context, "Thanks", ToastLength.Short).Show();
                }

                // Check if the only required permission has been granted
                
            }
            else
            {
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }
        }

        protected override void OnPause()
        {
            base.OnPause();
            _settings.KEYS_PATH = keysUIField.Text;
            _settings.DB_PATH = dbUIField.Text;
        }

        private async void _btnAsync()
        {
            _settings.KEYS_PATH = keysUIField.Text;
            _settings.DB_PATH = dbUIField.Text;

            PasswordSystem.Instance.Init(_settings);
            passwordUIFiled.Enabled = false;
            bool unlocked = await PasswordSystem.Instance.Unlock(passwordUIFiled.Text);
            passwordUIFiled.Enabled = true;
            if (unlocked)
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            }
            else
            {
                Toast.MakeText(Application.Context, "password incorrect", ToastLength.Short).Show();
            }
        }

      

    }
}
