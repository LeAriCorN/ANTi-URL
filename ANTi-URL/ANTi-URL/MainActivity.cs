using System;
using System.Collections.Generic;
using System.Threading;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ANTi_URL
{
    [Activity(Label = "Anti-URL")]
    public class MainActivity : Activity
    {
        bool switch_urlcopy = true;
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);
            ActionBar.Hide();

            mtoast = Toast.MakeText(this, "", ToastLength.Short);

            ClipboardManager clip = (ClipboardManager)GetSystemService(Context.ClipboardService);
            clip.PrimaryClipChanged += test;
                       

            if (switch_urlcopy == true)
            {
                clipboardautocopy(); //클립보드 복붙 함수
            }

            Button btn_launch_vt = FindViewById<Button>(Resource.Id.btn_launch_vt);
            Button btn_goto_setting = FindViewById<Button>(Resource.Id.btn_goto_setting);
            Button btn_goto_history = FindViewById<Button>(Resource.Id.btn_goto_history);
            //Switch seturlloading = FindViewById<Switch>(Resource.Id.switch_setting_urlloading);

                     
            btn_launch_vt.Click += changedownlabel;
            btn_goto_setting.Click += Btn_goto_setting_Click;
            btn_goto_history.Click += Btn_goto_history_Click;
            //seturlloading.CheckedChange += urlloadingsetting;


        }

        private void test(object sender, EventArgs e)
        {
            //mtoast.SetText("URL을 자동으로 붙여넣겠습니까?");
            //mtoast.Show();

            StartActivity(typeof(Go2App));

        }

        private void Btn_goto_setting_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(Setting));
        }

        private void Btn_goto_history_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(History));
        }

        private void urlloadingsetting(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            //TextView txtvurl = FindViewById<TextView>(Resource.Id.txtV_switch_urlloading);

            bool isChecked = e.IsChecked;
            if (isChecked)
            {
                //txtvurl.Text = "URL을 자동으로 입력합니다";
                switch_urlcopy = true;
            }
            else
            {
                //txtvurl.Text = "URL을 수동으로 입력합니다";
                switch_urlcopy = false;
            }
        }

        private void changedownlabel(object sender, System.EventArgs e)
        {
            EditText upeditor = FindViewById<EditText>(Resource.Id.txt_input_url);

            string res = upeditor.Text;

            if (checkkurl(res) == true)
            {
                ProgressDialog progress = new ProgressDialog(this);
                progress.Indeterminate = true;
                progress.SetProgressStyle(ProgressDialogStyle.Spinner);
                progress.SetMessage("URL을 분석중입니다");
                progress.SetCancelable(false);
                progress.Progress = 0;
                progress.Max = 100;
                progress.Show();

                int pg = 0;

                new Thread(new ThreadStart(delegate
                {
                    while (pg < 100)
                    {
                        pg += 30;
                        progress.Progress = pg;
                        Thread.Sleep(2000);
                    }
                    RunOnUiThread(() => { progress.Hide(); });

                })).Start();
            }
            else
            {
                mtoast.SetText("잘못된 URL을 입력했습니다");
                mtoast.Show();
            }

        }

        private bool checkkurl(string source)
        {
            return Android.Util.Patterns.WebUrl.Matcher(source).Matches();
        }
        
        private void clipboardautocopy()
        {
            EditText upeditor = FindViewById<EditText>(Resource.Id.txt_input_url);
            var clipboard = (Android.Content.ClipboardManager)GetSystemService(ClipboardService);

            var pData = "";

            if (!(clipboard.HasPrimaryClip))
            {
                // If it does contain data, decide if you can handle the data.

            }
            else if (!(clipboard.PrimaryClipDescription.HasMimeType(Android.Content.ClipDescription.MimetypeTextPlain)))
            {

                // since the clipboard has data but it is not plain text

            }
            else
            {
                //since the clipboard contains plain text.
                var item = clipboard.PrimaryClip.GetItemAt(0);

                // Gets the clipboard as text.
                pData = item.Text;
                
                if (checkkurl(pData) == true)
                {
                    upeditor.Text = pData.ToString();
                    mtoast.SetText("URL을 자동으로 입력했습니다");
                    mtoast.Show();
                }
                else
                {
                    mtoast.SetText("잘못된 URL을 입력했습니다");
                    mtoast.Show();
                }
            }
        }

        private static Toast mtoast;
        
    }
}
     

