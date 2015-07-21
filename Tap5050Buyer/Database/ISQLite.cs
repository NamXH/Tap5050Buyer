using System;
using SQLite.Net;

namespace Tap5050Buyer
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();

        bool Exists();

        void Delete();
    }
}

