using System;
using Newtonsoft.Json;

namespace Tap5050Buyer
{
    public class RaffleLocation
    {
        [JsonProperty(PropertyName = "location_id")]
        public string Id
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "location_name")]
        public string Name
        {
            get;
            set;
        }

        public RaffleLocation()
        {
        }
    }
}