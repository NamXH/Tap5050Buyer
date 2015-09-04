using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Tap5050Buyer
{
    // Messy properties. Do this to be compatible with server's api.
    public class Ticket
    {
        [JsonProperty(PropertyName = "image_url")]
        public string ImageUrl
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "raffle_name")]
        public string RaffleName
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "has_jackpot")]
        public string HasJackpot
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "event_id")]
        public int EventId
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "draw_date")]
        public DateTime DrawDate
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "event_end_time")]
        public DateTime EventEndTime 
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "licence_number")]
        public string LicenceNumber
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "raffle_type")]
        public string RaffleType
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "ticket_numbers")]
        public string TicketNumbersString
        {
            get;
            set;
        }

        [JsonIgnore]
        public List<string> TicketNumbers { get; set; }

        [JsonProperty(PropertyName = "winning_numbers")]
        public string WinningNumbersString
        {
            get;
            set;
        }

        [JsonIgnore]
        public List<string> WinningNumbers { get; set; }

        [JsonProperty(PropertyName = "purchase_date")]
        public string PurchaseDate
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "prize_description")]
        public string PrizeDescription
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "jackpot_total")]
        public int JackpotTotal
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "last_name")]
        public string LastName
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "buy_ticket_url")]
        public string BuyTicketUrl 
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "ticket_id")]
        public int TicketId
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "raffle_status")]
        public string RaffleStatus 
        {
            get;
            set;
        }
    }
}

