using System;
using SQLite.Net.Attributes;

namespace Tap5050Buyer
{
    public class Token
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Value { get; set; }
    }
}