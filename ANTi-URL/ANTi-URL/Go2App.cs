using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ANTi_URL
{
        [Activity(Theme ="@android:style/Theme.Translucent.NoTitleBar", NoHistory = true)]
        public class Go2App : Activity
        {
            protected override void OnCreate(Bundle bundle)
            {
                base.OnCreate(bundle);
                //ActionBar.Hide();
                SetContentView(Resource.Layout.Clipboard_Popup);

            var gotoapp = FindViewById<Button>(Resource.Id.goto_app);

            gotoapp.Click += Gotoapp_Click;

            new Thread(new ThreadStart(delegate
            {
                Thread.Sleep(1500);
                RunOnUiThread(() => {this.Finish(); });

            })).Start();

        }

        private void Gotoapp_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
            Finish();
        }
    }
}