using System;

namespace Tap5050Buyer
{
    // Very similar to Raffle Event. However, we have to make another class since the server API use different names in its tickets JSON -> bad, should be nested items !!
    public class RaffleEventForTickets
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public DateTime DrawDate { get; set; }

        public string LicenceNumber { get; set; }

        public string Type { get; set; }

        public string PrizeDescription { get; set; }

        public int JackpotTotal { get; set; }

        public string TicketNumberUrl
        {
            get;
            set;
        }

        public string WinningNumberUrl
        {
            get;
            set;
        }

        public string BuyTicketUrl 
        {
            get;
            set;
        }

        public int TicketId
        {
            get;
            set;
        }

        public string RaffleStatus 
        {
            get;
            set;
        }

        public DateTime EventEndTime 
        {
            get;
            set;
        }

        public string ImageUrl
        {
            get;
            set;
        }
    }
}