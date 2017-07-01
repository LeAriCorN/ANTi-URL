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
using Android.Util;
using ANTi_URL.Resources.Model;

namespace ANTi_URL.Resources.Datahelper
{
    public class Database
    {
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        public bool createDataBase()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "URL_Log.db")))
                {
                    connection.CreateTable<URL_Log>();
                    return true;
                }
            }
            catch(SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public bool InsertIntoTableURL_Log(URL_Log URL_Log)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "URL_Log.db")))
                {
                    connection.Insert(URL_Log);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public List<URL_Log> selectTableURL_Log()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "URL_Log.db")))
                {
                    return connection.Table<URL_Log>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return null;
            }
        }

        public bool UpdateTableURL_Log(URL_Log URL_Log)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "URL_Log.db")))
                {
                    connection.Query<URL_Log>("UPDATE URL_Log set Url=?,Result=? Where Id=?",URL_Log.Url,URL_Log.Result,URL_Log.Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public bool DeleteTableURL_Log(URL_Log URL_Log)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "URL_Log.db")))
                {
                    connection.Query<URL_Log>("DELETE FROM URL_Log");
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public bool SelectQueryTableURL_Log(string Id)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "URL_Log.db")))
                {
                    connection.Query<URL_Log>("SELECT * FROM URL_Log Where Id=?", Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }
    }
}