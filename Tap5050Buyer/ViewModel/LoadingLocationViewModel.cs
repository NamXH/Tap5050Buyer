using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tap5050Buyer
{
    public class LoadingLocationViewModel
    {
        internal const string c_serverBaseAddress = "http://dev.tap5050.com/";
        internal const string c_serverLocationApiAddress = "apex/tap5050_dev/Mobile_Web_Serv/locations";

        public static IList<RaffleLocation> RaffleLocations { get; set; }

        public GeonamesCountrySubdivision CountrySubdivision { get; set; }

        public LoadingLocationViewModel()
        {
        }

        public async Task GetCurrentLocationAndRaffleLocationList()
        {
            var updateGeolocationTask = GeolocationManager.UpdateGeolocation();
            var getRaffleLocationsTask = GetRaffleLocations();
            await Task.WhenAll(new List<Task>{ updateGeolocationTask, getRaffleLocationsTask });

            RaffleLocations = getRaffleLocationsTask.Result;
            CountrySubdivision = GeolocationManager.CountrySubdivision;
        }

        /// <summary>
        /// Gets the available raffle locations from Tap5050's server.
        /// </summary>
        /// <returns>The raffle locations.</returns>
        public async Task<List<RaffleLocation>> GetRaffleLocations()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(c_serverBaseAddress);

            HttpResponseMessage response = null;
            try
            {
                response = await client.GetAsync(c_serverLocationApiAddress);
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
                return JsonConvert.DeserializeObject<List<RaffleLocation>>(items.ToString());
            }
            return null;
        }
    }
}