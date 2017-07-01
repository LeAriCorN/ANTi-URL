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
using SQLite;

namespace ANTi_URL.Resources.Model
{
    public class URL_Log
    {
        [PrimaryKey,AutoIncrement]

        public int Id { get; set; }

        public string Url { get; set; }
        
        public string Result { get; set; }
    }
}