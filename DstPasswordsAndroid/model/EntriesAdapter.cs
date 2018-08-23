using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DstPasswordsCore.model.dataobjects;

namespace DstPasswordsAndroid.model
{
    class EntriesAdapter : ArrayAdapter<EncryptedEntry>
    {
        private readonly Context mContext;
        private List<EncryptedEntry> data = new List<EncryptedEntry>();

        public EntriesAdapter(Context context, List<EncryptedEntry> data) : base(context,0,data)
        {
            mContext = context;
            this.data = data;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View item = convertView;
            if (item == null)
            {
                item = LayoutInflater.From(mContext).Inflate(Resource.Layout.ListEntry, parent, false);
            }
            EncryptedEntry e = data[position];
            TextView name = item.FindViewById<TextView>(Resource.Id.listentrytext);
            name.Text = e.Id;
            return item;
        }
    }
}