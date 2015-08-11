using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Tap5050Buyer
{
    public class TicketListViewModel
    {
        internal const string c_serverBaseAddress = "https://dev.tap5050.com/";
        internal const string c_ticketsApiAddress = "apex/tap5050_dev/Mobile_Web_Serv/tickets";

        public List<Ticket> Tickets { get; set; }

        public IEnumerable<RaffleEventForTickets> EventForTicketsList { get; set; }

        public TicketListViewModel()
        {
        }

        public async Task<List<Ticket>> GetTickets()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(c_serverBaseAddress);

                HttpResponseMessage response = null;
                try // should be more fine grain maybe !!
                {
                    if (DatabaseManager.Token == null)
                    {
                        throw new Exception("Token is null while trying to retrieve tickets!");
                    }

                    var url = c_ticketsApiAddress + "?token_id=" + DatabaseManager.Token.Value;
                    response = await client.GetAsync(url);

                    var json = response.Content.ReadAsStringAsync().Result;

                    var obj = JsonConvert.DeserializeObject<JObject>(json);
                    var items = obj["items"];
                    if (items != null)
                    {
                        return JsonConvert.DeserializeObject<List<Ticket>>(items.ToString());
                    }
                    return null; 
                }
                catch (Exception e)
                {
                    throw new Exception("Error while retrieving account info:" + e.Message);
                }
            }
        }

        public async Task LoadData()
        {
            Tickets = await GetTickets();

            if (Tickets != null)
            {
                var raffleEventForTicketsDic = new Dictionary<int, RaffleEventForTickets>();

                foreach (var ticket in Tickets)
                {
                    if (!raffleEventForTicketsDic.ContainsKey(ticket.EventId))
                    {
                        raffleEventForTicketsDic.Add(ticket.EventId, new RaffleEventForTickets
                            {
                                Name = ticket.RaffleName,
                                Id = ticket.EventId,
                                DrawDate = ticket.DrawDate,
                                LicenceNumber = ticket.LicenceNumber,
                                Type = ticket.RaffleType,
                                PrizeDescription = ticket.PrizeDescription,
                                JackpotTotal = ticket.JackpotTotal,
                                WinningTicket = ticket.WinningTicket,
                            });
                    }
                }

                EventForTicketsList = raffleEventForTicketsDic.Values;
            }
        }
    }
}