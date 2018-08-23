using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Content;

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

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            _route(Pages.Unlock);

            
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

