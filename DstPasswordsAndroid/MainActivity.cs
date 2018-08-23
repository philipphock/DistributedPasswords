using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Content;
using DstPasswordsCore.model;
using Android.Util;
using DstPasswordsAndroid.model;
using System.Collections.Generic;
using DstPasswordsCore.model.dataobjects;
using System.Linq;

namespace DstPasswordsAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public enum Pages
        {
            Unlock
        }

        private ListView _entries;

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

                _entries = FindViewById<ListView>(Resource.Id.entries);
                List<EncryptedEntry> e = PasswordSystem.Instance.ReadDatabase().OrderBy(s => s.Id).ToList();

                EntriesAdapter a = new EntriesAdapter(Application.Context, e);
                _entries.Adapter = a;
                _entries.ItemClick += (s, ev) =>
                {
                    EncryptedEntry item = a.GetItem(ev.Position);
                    string ds = item.DecryptAsString();
                    Intent intent = new Intent(this, typeof(EntryView));
                    intent.PutExtra("asString", ds);
                    StartActivity(intent);


                };
                //_entries.Invalidate();
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

