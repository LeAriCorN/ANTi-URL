using Android.App;
using Android.Widget;
using Android.OS;

namespace ANTi_URL
{
    [Activity(Label = "ANTi_URL", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Button button = FindViewById<Button>(Resource.Id.gotit);

            button.Click += changedownlabel;

        }

        private void changedownlabel(object sender, System.EventArgs e)
        {
            EditText upeditor = FindViewById<EditText>(Resource.Id.editor);
            TextView showlabel = FindViewById<TextView>(Resource.Id.changelabel);

            string res = upeditor.Text;

            if (chkurl(res) == true)
            {
                showlabel.Text = res.ToString();
            }
            else
            {
                Toast.MakeText(this, "URL 형식에 맞지 않습니다", ToastLength.Long).Show();
            }


            //showlabel.Text = res.ToString();

        }

        private bool chkurl(string source)
        {
            return Android.Util.Patterns.WebUrl.Matcher(source).Matches();
        }
    }
}
     

