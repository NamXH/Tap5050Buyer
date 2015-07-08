using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tap5050Buyer
{
    public class RaffleListViewModel
    {
        public const string c_serverBaseAddress = "http://dev.tap5050.com/";
        public const string c_serverEventApiAddress = "apex/tap5050_dev/Mobile_Web_Serv/events";

        public IList<RaffleLocation> RaffleLocations { get; set; }

        public GeonamesCountrySubdivision CountrySubdivision { get; set; }

        public RaffleListViewModel(IList<RaffleLocation> raffleLocations, GeonamesCountrySubdivision countrySubdivision)
        {
            RaffleLocations = raffleLocations;
            CountrySubdivision = countrySubdivision;
        }

        public async Task<List<RaffleEvent>> GetRaffleEventsAtLocation(string locationName)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(c_serverBaseAddress);

            HttpResponseMessage response = null;
            try
            {
                response = await client.GetAsync(c_serverEventApiAddress + "?location=" + locationName);
            }
            catch (Exception)
            {
                return null;
            }
            var json = response.Content.ReadAsStringAsync().Result;

            var obj = JsonConvert.DeserializeObject<JObject>(json);
            var items = obj["items"];
            if (items != null)
            {
                return JsonConvert.DeserializeObject<List<RaffleEvent>>(items.ToString());
            }
            return null; 
        }

        public RaffleLocation MatchRaffleLocationWithCountrySubdivision(IList<RaffleLocation> raffleLocations, GeonamesCountrySubdivision countrySubdivision)
        {
            return raffleLocations.FirstOrDefault(x => x.Name == countrySubdivision.AdminName);
        }
    }
}