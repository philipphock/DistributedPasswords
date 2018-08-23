

using Android.App;
using Android.OS;
using Android.Widget;

namespace DstPasswordsAndroid
{
    [Activity(Label = "EntryView")]
    public class EntryView : Activity
    {
        private EditText content;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.entry_view);

            content = FindViewById<EditText>(Resource.Id.entryText);

            // Create your application here
        }

        protected override void OnResume()
        {
            base.OnResume();
            content.Text = Intent.GetStringExtra("asString");
        }
    }
}