using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace ANTi_URL
{
    [Activity(Label = "ANTi_URL")]
    public class MainActivity : Activity
    {
        int switch_urlcopy = 0;
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            mtoast = Toast.MakeText(this, "", ToastLength.Short);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            ActionBar.Hide();

            if (switch_urlcopy == 1)
            {
                clipboardautocopy(); //클립보드 복붙 함수
            }

            Button button = FindViewById<Button>(Resource.Id.gotit);
            Button btn_goto_setting = FindViewById<Button>(Resource.Id.btn_goto_setting);
            Switch seturlloading = FindViewById<Switch>(Resource.Id.switch_setting_urlloading);

         
            button.Click += changedownlabel;
            btn_goto_setting.Click += Btn_goto_setting_Click;
            seturlloading.CheckedChange += urlloadingsetting;


        }

        private void Btn_goto_setting_Click(object sender, System.EventArgs e)
        { 
            SetContentView(Resource.Layout.Setting);
        }

        private void urlloadingsetting(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            TextView txtvurl = FindViewById<TextView>(Resource.Id.txtV_switch_urlloading);

            bool isChecked = e.IsChecked;
            if (isChecked)
            {
                txtvurl.Text = "URL을 자동으로 입력합니다";
                switch_urlcopy = 0;
            }
            else
            {
                txtvurl.Text = "URL을 수동으로 입력합니다";
                switch_urlcopy = 1;
            }
        }

        private void changedownlabel(object sender, System.EventArgs e)
        {
            EditText upeditor = FindViewById<EditText>(Resource.Id.editor);
            TextView showlabel = FindViewById<TextView>(Resource.Id.changelabel);

            string res = upeditor.Text;

            if (checkkurl(res) == true)
            {
                showlabel.Text = res.ToString();
            }
            else
            {
                mtoast.SetText("올바르지 않은 URL 입니다");
                mtoast.Show();
            }

        }

        private bool checkkurl(string source)
        {
            return Android.Util.Patterns.WebUrl.Matcher(source).Matches();
        }
        
        private void clipboardautocopy()
        {
            EditText upeditor = FindViewById<EditText>(Resource.Id.editor);
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
            }
        }

        private static Toast mtoast;
    }
}
     

