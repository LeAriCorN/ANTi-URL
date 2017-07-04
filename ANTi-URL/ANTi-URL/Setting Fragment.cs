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

        int fun = 1;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AddPreferencesFromResource(Resource.Layout.Setting_Preference);
            mtoast = Toast.MakeText(Activity, "", ToastLength.Short);


            Preference pAppver = (Preference)FindPreference("App_ver");
            Preference pAppintro = (Preference)FindPreference("App_intro");
            Preference pFeedback = (Preference)FindPreference("Feedback");
            //Preference pOpensource = (Preference)FindPreference("OpenSource");

            pAppver.PreferenceClick += apverclick;
            pAppintro.PreferenceClick += Go2AppIntro;
            pFeedback.PreferenceClick += Go2Feedbackdialog;
            //pOpensource.PreferenceClick += Go2OpenSourceLicense;

        }

        private static Toast mtoast;

        private void apverclick(object sender, Preference.PreferenceClickEventArgs e)
        {
            fun += 1;
            switch (fun)
            {
                case 3: mtoast.SetText("지금 너랑 나랑 모든게 다 거짓말 같아"); mtoast.Show(); break;
                case 4: mtoast.SetText("너랑 일분 이분 시간이 다 거짓말 같아"); mtoast.Show(); break;
                case 5: mtoast.SetText("꿈일까? 아닐까? 몽롱한 기분인 걸"); mtoast.Show(); break;
                case 6: mtoast.SetText("네가 사랑한대 날"); mtoast.Show(); break;
                case 7: mtoast.SetText("어떡해 진짠가 봐"); mtoast.Show(); break;
                case 8: mtoast.SetText("또각또각 불빛 아래 두 사람"); mtoast.Show(); break;
                case 9: mtoast.SetText("빙글빙글 내 두 눈은 너를 따라"); mtoast.Show(); break;
                case 10: mtoast.SetText("사뿐사뿐 발을 맞춰 보는 나"); mtoast.Show(); break;
                case 11: mtoast.SetText("진짠가 봐 연인이 된 건가 봐"); mtoast.Show(); break;
                case 12: mtoast.SetText("지금,우리 - 러블리즈"); mtoast.Show(); fun = 0; Activity.SetContentView(Resource.Layout.easter); break;
            }


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