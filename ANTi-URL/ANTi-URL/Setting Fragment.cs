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
    public class Setting_Fragment : PreferenceFragment
    {

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AddPreferencesFromResource(Resource.Layout.Setting_Preference);
            

            Preference pAppintro = (Preference)FindPreference("App_intro");
            Preference pFeedback = (Preference)FindPreference("Feedback");
            Preference pOpensource = (Preference)FindPreference("OpenSource");
            

            pAppintro.PreferenceClick += Go2AppIntro;
            pFeedback.PreferenceClick += Go2Feedbackdialog;
            pOpensource.PreferenceClick += Go2OpenSourceLicense;

        }
        
        private void Go2Feedbackdialog(object sender, Preference.PreferenceClickEventArgs e)
        {
            AlertDialog.Builder builder;
            builder = new AlertDialog.Builder(this.Activity,Resource.Style.AlertDialogStyle);
            builder.SetTitle("피드백은 이쪽으로");
            builder.SetMessage("lazerbear77@gmail.com");
            builder.SetCancelable(true);
            builder.SetPositiveButton("확인",delegate{ });
            builder.Show();
        }

        private void Go2AppIntro(object sender, Preference.PreferenceClickEventArgs e)
        {
            Activity.StartActivity(typeof(AppIntro));
        }

        private void Go2OpenSourceLicense(object sender, Preference.PreferenceClickEventArgs e)
        {
            Activity.StartActivity(typeof(OpenSourceLicense));
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}