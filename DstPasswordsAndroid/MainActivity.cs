using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Content;
using DstPasswordsCore.model;
using Android.Util;

namespace DstPasswordsAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public enum Pages
        {
            Unlock
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            _init();



        }

        private void _init()
        {
            if (PasswordSystem.Instance.NotInitOrLocked())
            {
                _route(Pages.Unlock);
            }
            else
            {
                SetContentView(Resource.Layout.activity_main);

            }
        }

        protected override void OnResume()
        {
            base.OnResume();

            _init();
            

        }

        private void _route(Pages p)
        {
            System.Type page = null;
            switch (p)
            {
                case Pages.Unlock:
                    page = typeof(Unlock);
                    break;
            }
            

            if (page != null)
            {
                Intent intent = new Intent(this, page);
                StartActivity(intent);
            }
            else
            {
                Toast.MakeText(Application.Context, "No activity found", ToastLength.Short);
            }
            
        }
    }
}

