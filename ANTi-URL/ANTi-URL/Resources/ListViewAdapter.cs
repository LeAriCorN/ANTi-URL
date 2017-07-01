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
using Java.Lang;

namespace ANTi_URL.Resources
{
    public class ViewHolder : Java.Lang.Object
    {
        public TextView txtURL { get; set; }

        public TextView txtResult { get; set; }
    }


    public class ListViewAdapter : BaseAdapter
    {
        private Activity activity;
        private List<URL_Log> lstURL_Log;

        public ListViewAdapter(Activity activity, List<URL_Log> lstURL_Log)
        {
            this.activity = activity;
            this.lstURL_Log = lstURL_Log;
        }

        public override int Count
        {
            get
            {
                return lstURL_Log.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return lstURL_Log[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.db_layout, parent, false);

            var txtUrl = view.FindViewById<TextView>(Resource.Id.dbUrl);
            var txtResult = view.FindViewById<TextView>(Resource.Id.dbResult);

            txtUrl.Text = lstURL_Log[position].Url;
            txtResult.Text = lstURL_Log[position].Result;

            return view;
        }
    }
}