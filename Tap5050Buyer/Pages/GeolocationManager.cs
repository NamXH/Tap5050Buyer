using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Tap5050Buyer
{
    public static class GeolocationManager
    {
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

        public static async Task UpdateGeolocation()
        {
            Geolocator.StartListening(1000, 1);
            _geolocation = await Geolocator.GetPositionAsync(10000);
            Geolocator.StopListening();
        }

        // For future development
        //        public static DateTime LastUpdateGeolocationTime
        //        {
        //            get;
        //        }

    }
}