using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace Tap5050Buyer
{
    public partial class LoadingLocationPage : ContentPage
    {
        public const string c_loadingMessage = "Waiting for your current location.";
        public const string c_cannotReachServerErrorMessage = "Cannot contact server. Please check your Internet connection and try again.";

        public const string c_serverBaseAddress = "http://dev.tap5050.com/";
        public const string c_serverLocationApiAddress = "apex/tap5050_dev/Mobile_Web_Serv/locations";

        public LoadingLocationPage()
        {
            InitializeComponent();

            AddActivityIndicator();

            GetCurrentLocationAndRaffleLocationList();
        }

        // Have to make this func because we can't have async ctor
        public async void GetCurrentLocationAndRaffleLocationList()
        {
            var updateGeolocationTask = GeolocationManager.UpdateGeolocation();
            var getRaffleLocationsTask = GetRaffleLocations();
            await Task.WhenAll(new List<Task>{ updateGeolocationTask, getRaffleLocationsTask });

            var raffleLocations = getRaffleLocationsTask.Result;

            if (raffleLocations == null)
            {
                RemoveAllElement();
                AddTryAgainButton();
            }
            else
            {
                if ((GeolocationManager.CountrySubdivision != null) && (GeolocationManager.CountrySubdivision.AdminName != null))
                {
                    Navigation.PushAsync(new RaffleListPage(true, raffleLocations, GeolocationManager.CountrySubdivision));
                }
                else
                {
                    Navigation.PushAsync(new RaffleListPage(false, raffleLocations, null));
                }
            }
        }

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

        private void RemoveAllElement()
        {
            var count = layout.Children.Count - 1;
            for (int i = count; i >= 0; i--)
            {
                layout.Children.RemoveAt(i);
            }
        }

        private void AddActivityIndicator()
        {
            layout.Children.Add(new ActivityIndicator
                {
                    IsRunning = true
                });
            layout.Children.Add(new Label
                {
                    Text = c_loadingMessage
                });   
        }

        private void AddTryAgainButton()
        {
            var label = new Label
            {
                Text = c_cannotReachServerErrorMessage,
            };
            
            var tryAgainButton = new Button
            {
                Text = "Try Again",
                BorderColor = Color.Black,
                BorderWidth = 1
            };
            tryAgainButton.Clicked += (object sender, EventArgs e) =>
            {
                RemoveAllElement();
                AddActivityIndicator();
                GetCurrentLocationAndRaffleLocationList();
            };

            layout.Children.Add(label);
            layout.Children.Add(tryAgainButton);
        }
    }
}