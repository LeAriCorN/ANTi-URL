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

namespace ANTi_URL
{
    [Activity(Theme = "@style/noActionbar", Icon = "@drawable/icon")]
    public class History : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.History);
            var tolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(tolbar);
            ActionBar.Title = "과거 내역";
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);
            ActionBar.SetLogo(Resource.Drawable.icon);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                Finish();
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}