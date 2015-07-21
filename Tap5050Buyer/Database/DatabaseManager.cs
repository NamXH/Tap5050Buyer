using System;
using SQLite.Net;
using Xamarin.Forms;

namespace Tap5050Buyer
{
    public static class DatabaseManager
    {
        public static SQLiteConnection DbConnection { get; private set; }

        public static string Token { get; set; }

        static DatabaseManager()
        {
            var db = DependencyService.Get<ISQLite>();
        }
    }
}