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
            Tickets = tickets;
        }

        public async Task LoadData()
        {
            foreach (var ticket in Tickets)
            {
                ticket.TicketNumbers = await GetTicketNumbers(ticket.TicketNumberUrl);
            }

            TicketGroups = from ticket in Tickets
                                    group ticket by ticket.EventId into g
                                    select new TicketGroup { EventId = g.Key, Tickets = g.ToList() };

            foreach (var group in TicketGroups)
            {
                if (group.Tickets.Any())
                {
                    var winningNumbers = await GetWinningNumbers(group.Tickets.FirstOrDefault().WinningNumberUrl);
                    foreach (var ticket in group.Tickets)
                    {
                        ticket.WinningNumbers = winningNumbers;
                    }
                }
            }
        }

        public async Task<List<TicketNumber>> GetTicketNumbers(string ticketNumberUrl)
        {
            var client = new HttpClient();

            HttpResponseMessage response = null;
            try
            {
                response = await client.GetAsync(ticketNumberUrl);
            }
            catch (Exception e)
            {
                throw new Exception("Exception while getting ticket numbers: " + e.Message);
            }
            var json = response.Content.ReadAsStringAsync().Result;

            var obj = JsonConvert.DeserializeObject<JObject>(json);
            var items = obj["items"];
            if (items != null)
            {
                return JsonConvert.DeserializeObject<List<TicketNumber>>(items.ToString());
            }
            return null;
        }

        public async Task<List<WinningNumber>> GetWinningNumbers(string winningNumberUrl)
        {
            var client = new HttpClient();

            HttpResponseMessage response = null;
            try
            {
                response = await client.GetAsync(winningNumberUrl);
            }
            catch (Exception e)
            {
                throw new Exception("Exception while getting winning numbers: " + e.Message);
            }
            var json = response.Content.ReadAsStringAsync().Result;

            var obj = JsonConvert.DeserializeObject<JObject>(json);
            var items = obj["items"];
            if (items != null)
            {
                return JsonConvert.DeserializeObject<List<WinningNumber>>(items.ToString());
            }
            return null; 
        }
    }

    public class TicketGroup
    {
        public int EventId { get; set; }

        public List<Ticket> Tickets { get; set; }
    }
}