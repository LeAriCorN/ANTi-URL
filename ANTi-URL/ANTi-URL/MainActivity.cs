using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

//바이러스 토탈 api
using VirusTotalNET;
using VirusTotalNET.Objects;
using VirusTotalNET.ResponseCodes;
using VirusTotalNET.Results;

//SQLite
using ANTi_URL.Resources.Model;
using ANTi_URL.Resources.Datahelper;
using ANTi_URL.Resources;
using Android.Util;

namespace ANTi_URL
{
    [Activity(Label = "Anti-URL")]
    public class MainActivity : Activity
    {
        private static string ScanUrl;
        static bool hasUrlBeenScannedBefore;
        public static int fcnt;
        public static int total;

        Database db;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);
            ActionBar.Hide();

            mtoast = Toast.MakeText(this, "", ToastLength.Short);
            builder = new AlertDialog.Builder(this, Resource.Style.AlertDialogStyle);

            
            Button btn_launch_vt = FindViewById<Button>(Resource.Id.btn_launch_vt);
            Button btn_goto_setting = FindViewById<Button>(Resource.Id.btn_goto_setting);
            Button btn_goto_history = FindViewById<Button>(Resource.Id.btn_goto_history);
            //CheckBox chk_urllistener = FindViewById<CheckBox>(Resource.Id.chk_urllistener);

            db = new Database();
            db.createDataBase();
            
            
            //if (chk_urllistener.Checked)
            //{
            //    ClipboardManager clip = (ClipboardManager)GetSystemService(Context.ClipboardService);
            //    clip.PrimaryClipChanged += clipchange;
            //    //클립보드 리스너
            //}

            clipboardautocopy(); //클립보드 복붙 함수
            

            btn_launch_vt.Click += analyzingURL;
            btn_goto_setting.Click += Btn_goto_setting_Click;
            btn_goto_history.Click += Btn_goto_history_Click;

        }

        //private void clipchange(object sender, EventArgs e)
        //{
        //    StartActivity(typeof(Go2App));
        //}

        private void Btn_goto_setting_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(Setting));
        }

        private void Btn_goto_history_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(History));
        }

        private void analyzingURL(object sender, System.EventArgs e)
        {
            EditText upeditor = FindViewById<EditText>(Resource.Id.txt_input_url);

            ScanUrl = upeditor.Text;
            
            if (chkurl(ScanUrl))
            {
                ProgressDialog progress = new ProgressDialog(this, Resource.Style.AlertDialogStyle);
                progress.Indeterminate = true;
                progress.SetMessage("URL을 분석중입니다");
                progress.SetCancelable(true);
                progress.Show();

                new Thread(new ThreadStart(delegate
                {
                    runAPI().Wait();

                    RunOnUiThread(() => { progress.Hide(); URL_Repo(); inputDB(); });
                })).Start();
            }
            else
            {
                mtoast.SetText("  URL을 입력하지 않았거나 \n잘못된 URL을 입력했습니다");
                mtoast.Show();
            }

        }

        private void clipboardautocopy()
        {
            EditText upeditor = FindViewById<EditText>(Resource.Id.txt_input_url);
            var clipboard = (Android.Content.ClipboardManager)GetSystemService(ClipboardService);

            var pData = "";

            if (!(clipboard.HasPrimaryClip))
            {
                //데이터 존재시 핸들링

            }
            else if (!(clipboard.PrimaryClipDescription.HasMimeType(Android.Content.ClipDescription.MimetypeTextPlain)))
            {

                // 클립보드 내 데이터 플레인 텍스트 필터

            }
            else
            {
                //플레인 텍스트만 추출
                var item = clipboard.PrimaryClip.GetItemAt(0);

                pData = item.Text;

                if (chkurl(pData))
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

        private bool chkurl(string source)
        {
            return Android.Util.Patterns.WebUrl.Matcher(source).Matches();
        }

        private static Toast mtoast;
        private static AlertDialog.Builder builder;


        //API 부분
        private static async Task runAPI()
        {
            VirusTotal virusTotal = new VirusTotal("e94b6cd868bd18f84b422f0e5e3e353b794410c0e7449af2d946e346b92c1662");

            //https 사용
            virusTotal.UseTLS = true;

            UrlReport urlReport = await virusTotal.GetUrlReport(ScanUrl);

            hasUrlBeenScannedBefore = urlReport.ResponseCode == ReportResponseCode.Present;



            //If the url has been scanned before, the results are embedded inside the report.
            if (hasUrlBeenScannedBefore)
            {
                PrintScan(urlReport);
            }
            else
            {
                UrlScanResult urlResult = await virusTotal.ScanUrl(ScanUrl);
                //PrintScan(urlResult);
                await Task.Delay(500);

            }
        }
        
        private static void PrintScan(UrlReport urlReport)
        {
            int temp1 = 0;

            fcnt = 0;
            total = urlReport.Total;

            if (urlReport.ResponseCode == ReportResponseCode.Present)
            {
                foreach (KeyValuePair<string, ScanEngine> scan in urlReport.Scans)
                {
                    if (scan.Value.Detected)
                    {
                        temp1 += 1;
                    }
                }
                fcnt = temp1;
            }


        }

        private void URL_Repo()
        {
            string aTitle = ""; string aMsg = "";

            if (fcnt < 3)
            {
                aTitle = "안전합니다!";
                aMsg = "탐지 결과 : "+fcnt+" / "+total+"\n" + "" + total + "개의 검사 중 " + fcnt + "개에서 악성으로 판정";
            }
            else if (fcnt > 6)
            {
                aTitle = "불안합니다!";
                aMsg = "탐지 결과 : " + fcnt + " / " + total + "\n" + "" + total + "개의 검사 중 " + fcnt + "개에서 악성으로 판정";
            }
            else if (fcnt > 10)
            {
                aTitle = "초조합니다!";
                aMsg = "탐지 결과 : " + fcnt + " / " + total + "\n" + "" + total + "개의 검사 중 " + fcnt + "개에서 악성으로 판정";
            }
            else if (fcnt > 20)
            {
                aTitle = "절망적입니다!";
                aMsg = "탐지 결과 : " + fcnt + " / " + total + "\n" + "" + total + "개의 검사 중 " + fcnt + "개에서 악성으로 판정";
            }
            else if (fcnt > 40)
            {
                aTitle = "파국입니다!";
                aMsg = "탐지 결과 : " + fcnt + " / " + total + "\n" + "" + total + "개의 검사 중 " + fcnt + "개에서 악성으로 판정";
            }
            else
            {
                aTitle = "대국적으로 포맷을 하십시오!";
                aMsg = "탐지 결과 : " + fcnt + " / " + total + "\n" + "" + total + "개의 검사 중 " + fcnt + "개에서 악성으로 판정";
            }

            builder.SetTitle(aTitle);
            builder.SetMessage(aMsg);
            builder.SetCancelable(true);
            builder.SetPositiveButton("확인", delegate { });
            builder.Show();
        }

        //DB 입력 부분
        public void inputDB()
        {
            TextView txt_input_url = FindViewById<TextView>(Resource.Id.txt_input_url);

            URL_Log log = new URL_Log()
            {
                Url = txt_input_url.Text,
                Result = fcnt + " / " + total
            };
            db.InsertIntoTableURL_Log(log);
        }

    }
}


