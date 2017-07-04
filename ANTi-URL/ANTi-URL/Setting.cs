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
using Android.Preferences;

namespace ANTi_URL
{
    [Activity(Theme = "@style/noActionbar", Icon = "@drawable/icon")]
    public class Setting : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Setting);
            FragmentManager.BeginTransaction().Add(Resource.Id.preferenceframe, new Setting_Fragment()).Commit();
            var tolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(tolbar);
            ActionBar.Title = "메뉴";
            ActionBar.SetDisplayShowHomeEnabled(true);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            
        }


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home : Finish(); return true;
            }
            return base.OnOptionsItemSelected(item);
        }
        
    }
}