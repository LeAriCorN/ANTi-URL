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
            ActionBar.Title = "내 곁에 남아줄래";
            //ActionBar.SetIcon(Resource.Drawable.icon);
        }
    }
}