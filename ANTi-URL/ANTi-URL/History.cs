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
using ANTi_URL.Resources.Model;
using ANTi_URL.Resources.Datahelper;
using ANTi_URL.Resources;
using Android.Util;

namespace ANTi_URL
{
    [Activity(Theme = "@style/noActionbar", Icon = "@drawable/icon")]
    public class History : Activity
    {
        ListView lstData;
        List<URL_Log> lstSource = new List<URL_Log>();
        Database db;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.History);
            var tolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(tolbar);
            ActionBar.Title = "과거 내역";
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);

            //db 생성
            db = new Database();
            db.createDataBase();


            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            Log.Info("DB_PATH", folder);

            lstData = FindViewById<ListView>(Resource.Id.db_list);
            
            var dbDel = FindViewById<Button>(Resource.Id.dbDel);


            //데이터 로드
            LoadData();

            //이벤트
            dbDel.Click += delegate
            {
                URL_Log log = new URL_Log();
                db.DeleteTableURL_Log(log);
                LoadData();
            };

            lstData.ItemClick += (s, e) =>
            {
                for (int i = 0; i < lstData.Count; i++)
                {
                    if (e.Position == i)
                    {
                        lstData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.DarkGray);
                    }
                    else
                        lstData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Transparent);
                }

            };

        }

        private void LoadData()
        {
            lstSource = db.selectTableURL_Log();
            var adapter = new ListViewAdapter(this,lstSource);
            lstData.Adapter = adapter;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                Finish();
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}