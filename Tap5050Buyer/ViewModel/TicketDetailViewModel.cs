using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tap5050Buyer
{
    public class TicketDetailViewModel
    {
        internal const string c_serverBaseAddress = "https://dev.tap5050.com/";
        internal const string c_ticketsApiAddress = "apex/tap5050_dev/Mobile_Web_Serv/tickets";

        public List<Ticket> Tickets { get; set; }

        public IEnumerable<TicketGroup> TicketGroups { get; set; }

        public TicketDetailViewModel(List<Ticket> tickets)
        {
            TicketGroups = from ticket in tickets
                                    group ticket by ticket.EventId into g
                                    select new TicketGroup { EventId = g.Key, Tickets = g.ToList() }; 
        }
    }

    public class TicketGroup
    {
        public int EventId { get; set; }

        public List<Ticket> Tickets { get; set; }
    }
}