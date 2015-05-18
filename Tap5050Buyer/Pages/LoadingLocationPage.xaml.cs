using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Net.Http;

namespace Tap5050Buyer
{
    public partial class LoadingLocationPage : ContentPage
    {
        public const string c_loadingMessage = "Waiting for your current location.";
        public const string c_cannotReachServerErrorMessage = "Cannot contact server. Please check your Internet connection and try again.";

        public int a
        {
            get;
            set;
        }

        public LoadingLocationPage()
        {
            InitializeComponent();

            AddActivityIndicator();

            GetCurrentLocationAndRaffleLocationList();
        }

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
                if (GeolocationManager.Geolocation != null)
                {
                }
            }
        }

        public async Task<List<string>> GetRaffleLocations()
        {
            // Placeholder while waiting for server implementation
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://api.geonames.org/");
            var response = await client.GetAsync("countrySubdivision?lat=37&lng=-122&username=namhoang");
            var json = response.Content.ReadAsStringAsync().Result;
            Debug.WriteLine(json);

            if (a == 1)
            {
                a = 0;
                return new List<string>
                {
                    "California",
                    "Saskatchewan",
                    "Alberta",
                    "Ontaria"
                };
            }
            else
            {
                a = 1;
                return null;
            }
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