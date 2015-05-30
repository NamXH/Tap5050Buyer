using System;
using Newtonsoft.Json;

namespace Tap5050Buyer
{
    public class RaffleEvent
    {
        [JsonProperty(PropertyName = "event_id")]
        public int Id {
            get;
            set;
        }

        [JsonProperty(PropertyName = "event_name")]
        public string Name {
            get;
            set;
        }

        [JsonProperty(PropertyName = "description")]
        public string Description
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "has_jackpot")]
        public string HasJackpot {
            get;
            set;
        }

        [JsonProperty(PropertyName = "prize_url")]
        public string PrizeUrl {
            get;
            set;
        }

        [JsonProperty(PropertyName = "jackpot_description")]
        public string JackpotDescription {
            get;
            set;
        }

        [JsonProperty(PropertyName = "buy_ticket_url")]
        public string BuyTicketUrl {
            get;
            set;
        }

        [JsonProperty(PropertyName = "image_url")]
        public string ImageUrl {
            get;
            set;
        }

        [JsonProperty(PropertyName = "jackpot_total")]
        public string JackpotTotal {
            get;
            set;
        }

        [JsonProperty(PropertyName = "user_id")]
        public string UserId {
            get;
            set;
        }

        [JsonProperty(PropertyName = "organization")]
        public string Organization {
            get;
            set;
        }

        [JsonProperty(PropertyName = "location_name")]
        public string LocationName {
            get;
            set;
        }

        [JsonProperty(PropertyName = "location_id")]
        public string LocationId {
            get;
            set;
        }

        [JsonProperty(PropertyName = "licence_number")]
        public string LicenceNumber {
            get;
            set;
        }

        public RaffleEvent()
        {
        }
    }
}