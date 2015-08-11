using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Tap5050Buyer
{
    public class TicketsViewModel
    {
        internal const string c_serverBaseAddress = "https://dev.tap5050.com/";
        internal const string c_ticketsApiAddress = "apex/tap5050_dev/Mobile_Web_Serv/tickets";

        public List<Ticket> Tickets { get; set; }

        public TicketsViewModel()
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
                    // Comment out to test !!
//                    if (DatabaseManager.Token == null)
//                    {
//                        throw new Exception("Token is null while trying to retrieve data!");
//                    }

//                    var url = c_ticketsApiAddress + "?token_id=" + DatabaseManager.Token.Value;
                    var url = c_ticketsApiAddress + "?token_id=" + "7RDRAKNMEOURGL0T0020";
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
        }
    }
}