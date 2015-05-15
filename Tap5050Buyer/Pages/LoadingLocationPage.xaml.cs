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
        public Position CurrentPosition
        {
            get;
            set;
        }

        public LoadingLocationPage()
        {
            InitializeComponent();

            GetCurrentLocationAndRaffleLocationList();
            Debug.WriteLine("abc");
            if (CurrentPosition != null)
            {
                Debug.WriteLine(CurrentPosition.Longitude);
                Debug.WriteLine(CurrentPosition.Latitude);
            }
        }



        public async void GetCurrentLocationAndRaffleLocationList()
        {
            await Task.WhenAll(new List<Task>{ GetCurrentLocation(), Foo() });
        }

        public async Task GetCurrentLocation()
        {
            var geolocator = DependencyService.Get<IGeolocator>();
            geolocator.StartListening(1000, 1);
            CurrentPosition = await geolocator.GetPositionAsync(10000);
            Debug.WriteLine(CurrentPosition.Longitude);
            Debug.WriteLine(CurrentPosition.Latitude);
            geolocator.StopListening();
        }

        public async Task Foo()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://api.geonames.org/");
            var response = await client.GetAsync("countrySubdivision?lat=37&lng=-122&username=namhoang");
            var json = response.Content.ReadAsStringAsync().Result;
            Debug.WriteLine(json);
        }
    }
}