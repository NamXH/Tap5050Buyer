using System;
using SQLite.Net;
using Xamarin.Forms;

namespace Tap5050Buyer
{
    public static class DatabaseManager
    {
        public static SQLiteConnection DbConnection { get; private set; }

        public static Token Token { get; set; }

        static DatabaseManager()
        {
            var db = DependencyService.Get<ISQLite>();
            db.Delete(); // For test

            if (!db.Exists())
            {
                DbConnection = db.GetConnection();
                SetupSchema();
            }
            else
            {
                DbConnection = db.GetConnection();
            }
        }

        private static void SetupSchema()
        {
            DbConnection.CreateTable<Token>();
        }

        public static Token GetFirstToken()
        {
            return DatabaseManager.DbConnection.Table<Token>().FirstOrDefault();
        }
    }
}