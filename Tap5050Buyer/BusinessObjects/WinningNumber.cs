using System;
using Newtonsoft.Json;

namespace Tap5050Buyer
{
    public class WinningNumber
    {
        [JsonProperty(PropertyName = "winning_number")]
        public string Number
        {
            get;
            set;
        }
    }
}

