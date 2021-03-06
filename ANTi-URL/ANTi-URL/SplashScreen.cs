﻿using System;
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
    [Activity(Label = "ANTi-URL" , MainLauncher = true, Theme="@style/Theme.Splash", NoHistory=true, Icon="@drawable/icon")]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Thread.Sleep(900);
            StartActivity(typeof(MainActivity));
            Finish();
        }
    }
}