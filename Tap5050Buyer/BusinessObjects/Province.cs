using System;
using Newtonsoft.Json;
using SQLite.Net.Attributes;

namespace Tap5050Buyer
{
    public class Province
    {
        [PrimaryKey, AutoIncrement, JsonIgnore]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "state_province_name")]
        public string ProvinceName
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "country_code")]
        public string CountryCode
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "state_province_abbrev")]
        public string ProvinceAbbreviation
        {
            get;
            set;
        }
    }
}

