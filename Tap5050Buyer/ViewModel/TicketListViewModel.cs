using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using Xamarin.Forms;

namespace Tap5050Buyer
{
    public class TicketListViewModel
    {
        internal const string c_serverBaseAddress = "https://dev.tap5050.com/";
        internal const string c_ticketsApiAddress = "apex/tap5050_dev/Mobile_Web_Serv/tickets";
        internal const string c_userApiAddress = "apex/tap5050_dev/Mobile_Web_Serv/users";

        public List<Ticket> Tickets { get; set; }

        public IEnumerable<RaffleEventForTickets> EventForTicketsList { get; set; }

        public UserAccount UserAccount { get; set; }

        public TicketListViewModel()
        {
        }

        public async Task<List<Ticket>> GetTickets()
        {
            if (DatabaseManager.Token == null)
            {
                throw new Exception("Token is null while trying to retrieve tickets!");
            }

            var result = await ServerCaller.ExtendTokenAsync(DatabaseManager.Token.Value);

            if (result.Item1)
            {
                var url = c_serverBaseAddress + c_ticketsApiAddress + "?token_id=" + DatabaseManager.Token.Value;
                var json = await DependencyService.Get<IWebRequestProtocolVersion10>().GetResponseStringAsync(url);

                if (json == String.Empty)
                {
                    return null;
                }

                JObject obj;
                try
                {
                    obj = JsonConvert.DeserializeObject<JObject>(json);
                }
                catch (Exception e)
                {
                    throw new Exception("Error when deserializing server json for tickets.", e);
                }

                var items = obj["items"];
                if (items != null)
                {
                    return JsonConvert.DeserializeObject<List<Ticket>>(items.ToString());
                }
                return null; 
            }
            else
            {
                // Handle invalid or expired token !!
                DatabaseManager.DeleteToken();
                MessagingCenter.Send<TicketListViewModel>(this, "Token Deleted");
                return null;
            }
        }

        public async Task LoadData()
        {
            Tickets = await GetTickets();

            if (Tickets != null)
            {
                foreach (var ticket in Tickets)
                {
                    if (!String.IsNullOrWhiteSpace(ticket.TicketNumbersString))
                    {
                        ticket.TicketNumbers = ticket.TicketNumbersString.Trim().Split(' ').ToList();
                    }

                    if (!String.IsNullOrWhiteSpace(ticket.WinningNumbersString))
                    {
                        ticket.WinningNumbers = ticket.WinningNumbersString.Trim().Split(' ').ToList();
                    }
                }

                await GetAccountInfo(); // Not very good here!! We get Account Info here only to check if his phone has been verified or not!!

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
                                ImageUrl = ticket.ImageUrl,
                            });
                    }
                }

                EventForTicketsList = raffleEventForTicketsDic.Values;
            }
            else
            {
                EventForTicketsList = Enumerable.Empty<RaffleEventForTickets>();
            }
        }

        public async Task GetAccountInfo()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(c_serverBaseAddress);

                HttpResponseMessage response = null;
                try // should be more fine grain maybe !!
                {
                    if (DatabaseManager.Token == null)
                    {
                        throw new Exception("Token is null while trying to retrieve data!");
                    }

                    // Don't have to worry about expired token here since we have handled it in GetTickets()
                    var url = c_userApiAddress + "?token_id=" + DatabaseManager.Token.Value;

                    response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = response.Content.ReadAsStringAsync().Result;

                        UserAccount = JsonConvert.DeserializeObject<UserAccount>(json);

                        // Translate from code to full name
                        var country = DatabaseManager.DbConnection.Table<Country>().Where(x => x.CountryCode == UserAccount.CountryCode).FirstOrDefault();
                        if (country != null)
                        {
                            UserAccount.Country = country.CountryName;
                        }

                        var province = DatabaseManager.DbConnection.Table<Province>().Where(x => x.ProvinceAbbreviation == UserAccount.ProvinceAbbreviation).FirstOrDefault();
                        if (province != null)
                        {
                            UserAccount.Province = province.ProvinceName;
                        }
                    }
                    else
                    {
                        UserAccount = null;
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Error while retrieving account info: " + e.Message, e);
                }
            }
        }
    }
}