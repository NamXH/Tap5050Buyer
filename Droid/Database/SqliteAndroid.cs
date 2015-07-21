using System;
using System.IO;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using Xamarin.Forms;

[assembly: Dependency(typeof(Tap5050Buyer.Droid.SqliteAndroid))]
namespace Tap5050Buyer.Droid
{
    public class SqliteAndroid : ISQLite
    {
        private const string c_dbName = "tap5050.db";

        public string DbPath
        {
            get
            {
                var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                return Path.Combine(documentsDirectory, c_dbName);
            }
        }

        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(new SQLitePlatformAndroid(), DbPath);
        }

        public bool Exists()
        {
            return File.Exists(DbPath);
        }

        public void Delete()
        {
            if (File.Exists(DbPath))
            {
                File.Delete(DbPath);
            }
        }
    }
}

