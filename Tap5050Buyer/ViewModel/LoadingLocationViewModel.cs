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
        internal const string c_serverBaseAddress = "https://www.tap5050.com/";
        internal const string c_serverLocationApiAddress = "apex/tap5050_test/Mobile_Web_Serv/locations";
        internal const string c_countriesApiAddress = "apex/tap5050_test/Mobile_Web_Serv/countries";
        internal const string c_provincesApiAddress = "apex/tap5050_test/Mobile_Web_Serv/states_provinces";

        public static IList<RaffleLocation> RaffleLocations { get; set; }

        public static bool FailToGetCountriesOrProvinces { get; set; }

        // Don't make this static so we only use GeolocationManager instead.
        public GeonamesCountrySubdivision CountrySubdivision { get; set; }

        public static bool IsLocationDetected { get; set; }

        public static RaffleLocation UserSelectedLocation { get; set; }

        public LoadingLocationViewModel()
        {
        }

        public async Task GetCurrentLocationAndServerData()
        {
            var updateGeolocationTask = GeolocationManager.UpdateGeolocation();
            var getRaffleLocationsTask = GetRaffleLocations();
            var tasks = new List<Task>{ updateGeolocationTask, getRaffleLocationsTask };

            bool isGetCountriesAndProvinces = false;
            var getCountries = GetCountries();
            var getProvinces = GetProvinces();
            FailToGetCountriesOrProvinces = false;

            if ((DatabaseManager.DbConnection.Table<Country>().Count() == 0) || (DatabaseManager.DbConnection.Table<Province>().Count() == 0))
            {
                isGetCountriesAndProvinces = true;
            }

            if (isGetCountriesAndProvinces)
            {
                tasks.Add(getCountries);
                tasks.Add(getProvinces); 
            }

            await Task.WhenAll(tasks);

            RaffleLocations = getRaffleLocationsTask.Result;
            if (RaffleLocations != null)
            {
                // Set default value
                UserSelectedLocation = RaffleLocations[0];
            }
            CountrySubdivision = GeolocationManager.CountrySubdivision;

            if ((CountrySubdivision != null) && (CountrySubdivision.AdminName != null))
            {
                IsLocationDetected = true;
            }
            else
            {
                IsLocationDetected = false;
            }

            if (isGetCountriesAndProvinces)
            {
                var countries = getCountries.Result;
                var provinces = getProvinces.Result;

                if ((countries == null) || (provinces == null))
                {
                    FailToGetCountriesOrProvinces = true;
                }
                else
                {
                    foreach (var country in countries)
                    {
                        DatabaseManager.DbConnection.Insert(country);
                    }
                    foreach (var province in provinces)
                    {
                        DatabaseManager.DbConnection.Insert(province);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the available raffle locations from Tap5050's server.
        /// </summary>
        /// <returns>The raffle locations.</returns>
        public async Task<List<RaffleLocation>> GetRaffleLocations()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(c_serverBaseAddress);

            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync(c_serverLocationApiAddress);
            }
            catch
            {
                return null;
            }

            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;

                var obj = JsonConvert.DeserializeObject<JObject>(json);
                var items = obj["items"];
                if (items != null)
                {
                    return JsonConvert.DeserializeObject<List<RaffleLocation>>(items.ToString());
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Country>> GetCountries()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(c_serverBaseAddress);

            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync(c_countriesApiAddress);
            }
            catch
            {
                return null;
            }

            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;

                var obj = JsonConvert.DeserializeObject<JObject>(json);
                var items = obj["items"];
                if (items != null)
                {
                    return JsonConvert.DeserializeObject<List<Country>>(items.ToString());
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Province>> GetProvinces()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(c_serverBaseAddress);

            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync(c_provincesApiAddress);
            }
            catch
            {
                return null;
            }

            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;

                var obj = JsonConvert.DeserializeObject<JObject>(json);
                var items = obj["items"];
                if (items != null)
                {
                    return JsonConvert.DeserializeObject<List<Province>>(items.ToString());
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}