using System;
using Newtonsoft.Json;

namespace Tap5050Buyer
{
    public class TicketNumber
    {
        [JsonProperty(PropertyName = "ticket_number")]
        public string Number
        {
            get;
            set;
        }
    }
}

