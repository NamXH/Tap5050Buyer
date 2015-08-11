using System;
using Newtonsoft.Json;

namespace Tap5050Buyer
{
    public class Ticket
    {
        [JsonProperty(PropertyName = "raffle_name")]
        public string RaffleName
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

        [JsonProperty(PropertyName = "ticket_number")]
        public string TicketNumber 
        {
            get;
            set;
        }

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

        [JsonProperty(PropertyName = "winning_ticket")]
        public string WinningTicket
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
    }
}

