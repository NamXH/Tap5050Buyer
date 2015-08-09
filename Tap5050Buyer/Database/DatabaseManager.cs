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
//            db.Delete(); // For test

            if (!db.Exists())
            {
                DbConnection = db.GetConnection();
                SetupSchema();
            }
            else
            {
                DbConnection = db.GetConnection();

                // Download countries, provinces every time user starts the app by deleting the old ones
                DbConnection.DeleteAll<Province>();
                DbConnection.DeleteAll<Country>();
            }
        }

        private static void SetupSchema()
        {
            DbConnection.CreateTable<Token>();
            DbConnection.CreateTable<Country>();
            DbConnection.CreateTable<Province>();
        }

        public static Token GetFirstToken()
        {
            return DatabaseManager.DbConnection.Table<Token>().FirstOrDefault();
        }

        public static void DeleteToken()
        {
            DbConnection.DeleteAll<Token>();
            Token = null;
        }

        public static Token InsertToken(string tokenValue)
        {
            DbConnection.Insert(new Token
                {
                    Value = tokenValue,
                });

            Token = DatabaseManager.GetFirstToken(); // Do this instead of taking the Token directly to make sure there's only 1 Token object. Maybe not neccessary though.

            return Token;
        }
    }
}