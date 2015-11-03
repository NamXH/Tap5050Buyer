using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using System.IO;

namespace Tap5050Buyer
{
    public class RaffleListViewModel
    {
        public const string c_serverBaseAddress = "https://dev.tap5050.com/";
        public const string c_serverEventApiAddress = "apex/tap5050_dev/Mobile_Web_Serv/events";

        public RaffleLocation RaffleLocation { get; set; }

        public string LocationName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tap5050Buyer.RaffleListViewModel"/> class.
        /// </summary>
        /// <param name="raffleLocations">Raffle locations.</param>
        /// <param name="locationDetected">If set to <c>true</c> location detected.</param>
        /// <param name="userSelectedLocation">User selected location (only available if location is not detected)</param>
        /// <param name="countrySubdivision">Country subdivision of the currrent GeoLocation. Can be null if location is not detected</param>
        public RaffleListViewModel(bool isLocationDetected, IList<RaffleLocation> raffleLocations, RaffleLocation userSelectedLocation, GeonamesCountrySubdivision countrySubdivision)
        {
            if (isLocationDetected)
            {
                LocationName = countrySubdivision.AdminName;
                RaffleLocation = MatchRaffleLocationWithCountrySubdivision(raffleLocations, countrySubdivision);
            }
            else
            {
                LocationName = userSelectedLocation.Name;
                RaffleLocation = userSelectedLocation;
            }
        }

        public async Task<List<RaffleEvent>> GetRaffleEventsAtLocation(string locationName)
        {
            var url = c_serverBaseAddress + c_serverEventApiAddress + "?location=" + locationName;

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            // request.ProtocolVersion is not PCL compatible

            var response = (HttpWebResponse)await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, null);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                StringBuilder stringBuilder = new StringBuilder(); 
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null) // Follow Nan Chen's Tap5050Seller app solution
                    {
                        stringBuilder.Append(line);
                    }
                }
                var json = stringBuilder.ToString();  

                JObject obj;
                try
                {
                    obj = JsonConvert.DeserializeObject<JObject>(json);
                }
                catch (Exception e)
                {
                    throw new Exception("Error when deserializing server json.", e);
                }

                var items = obj["items"];
                if (items != null)
                {
                    return JsonConvert.DeserializeObject<List<RaffleEvent>>(items.ToString());
                }
                return null; 
            }
            else
            {
                return null;
            }
        }

        public RaffleLocation MatchRaffleLocationWithCountrySubdivision(IList<RaffleLocation> raffleLocations, GeonamesCountrySubdivision countrySubdivision)
        {
            return raffleLocations.FirstOrDefault(x => x.Name == countrySubdivision.AdminName);
        }
    }
}