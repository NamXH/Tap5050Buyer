using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Tap5050Buyer
{
    public static class GeolocationManager
    {
        private static readonly string _geonamesUsername = "namhoang";
        private static readonly string _reverseGeocodingServiceBaseUri = "http://api.geonames.org/";

        private static IGeolocator _geolocator;

        public static IGeolocator Geolocator
        {
            get
            {
                if (_geolocator == null)
                {
                    _geolocator = DependencyService.Get<IGeolocator>();
                }
                return _geolocator;
            }
        }

        private static Position _geolocation;

        public static Position Geolocation
        {
            get
            {
                return _geolocation;
            }
        }

        private static GeonamesCountrySubdivision _countrySubdivision;

        public static GeonamesCountrySubdivision CountrySubdivision
        {
            get
            {
                return _countrySubdivision;
            }
        }

        public static async Task UpdateGeolocation()
        {
            // Get device lat, long
            Geolocator.StartListening(1000, 1);
            _geolocation = await Geolocator.GetPositionAsync(10000);
            Geolocator.StopListening();

            // Get location name from lat, long
            if (_geolocation != null)
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(_reverseGeocodingServiceBaseUri);
                var endpointAddress = String.Format("countrySubdivisionJSON?lat={0}&lng={1}&username={2}", _geolocation.Latitude, _geolocation.Longitude, _geonamesUsername);

                HttpResponseMessage response = null;
                try
                {
                    response = await client.GetAsync(endpointAddress);
                }
                catch (Exception e)
                {
                    _countrySubdivision = null;
                    return;
                }

                var json = response.Content.ReadAsStringAsync().Result;
                _countrySubdivision = JsonConvert.DeserializeObject<GeonamesCountrySubdivision>(json);
            }
        }
    }
}