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
    public class OpenSource : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.OpenSource);
            var tolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(tolbar);
            ActionBar.Title = "오픈소스 라이센스";
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);
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