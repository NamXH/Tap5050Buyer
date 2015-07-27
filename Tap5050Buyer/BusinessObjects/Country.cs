using System;
using Newtonsoft.Json;
using SQLite.Net.Attributes;

namespace Tap5050Buyer
{
    public class Country
    {
        [PrimaryKey, AutoIncrement, JsonIgnore]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "country_code")]
        public string CountryCode
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "country_name")]
        public string CountryName 
        {
            get;
            set;
        }
    }
}

