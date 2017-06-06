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
        bool Clipboard_Listen = true;
        bool Clipboard_Paste=true;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AddPreferencesFromResource(Resource.Layout.Setting_Preference);

            SwitchPreference pClipboardlisten = (SwitchPreference)FindPreference("Clipboard_Listen");
            SwitchPreference pClipboardPaste = (SwitchPreference)FindPreference("Clipboard_Paste");

            Preference pAppintro = (Preference)FindPreference("App_intro");
            Preference pFeedback = (Preference)FindPreference("Feedback");
            Preference pOpensource = (Preference)FindPreference("OpenSource");

            //pClipboardlisten.PreferenceClick += OnoffClipboardListen;
            pClipboardlisten.PreferenceChange += ChangeURLListenerStatus;
            pClipboardPaste.PreferenceChange += ChangeURLPasteStatus;

            pAppintro.PreferenceClick += Go2AppIntro;
            pFeedback.PreferenceClick += Go2Feedbackdialog;
            pOpensource.PreferenceClick += Go2OpenSourceLicense;

        }

        private void ChangeURLPasteStatus(object sender, Preference.PreferenceChangeEventArgs e)
        {
            ISharedPreferences pref = Application.Context.GetSharedPreferences("bool", FileCreationMode.Private);
            ISharedPreferencesEditor edit = pref.Edit();
            
            if (Clipboard_Listen)
            {
                edit.PutBoolean("Clipboard_Listen", false);
                Clipboard_Listen = pref.GetBoolean("Clipboard_Listen", false);
                edit.Commit();
            }
            else
            {
                edit.PutBoolean("Clipboard_Listen", true);
                Clipboard_Listen = pref.GetBoolean("Clipboard_Listen", true);
                edit.Commit();
            }
        }

        private void ChangeURLListenerStatus(object sender, Preference.PreferenceChangeEventArgs e)
        {
            
        }

        private void OnoffClipboardListen(object sender, Preference.PreferenceClickEventArgs e)
        {
            
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
            Activity.StartActivity(typeof(OpenSource));
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}