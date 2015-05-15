using System;
using Xamarin.Forms;
using Xamarin.Forms.Labs.Services.Geolocation;

namespace Tap5050Buyer
{
    public static class GeolocationUtility
    {
        public static IGeolocator Geolocator
        {
            get;
            set;
        }

        private static Position _currentPosition;

        public static Position CurrentPosition
        {
            get
            {
                if (_currentPosition == null)
                {
//                    Geolocator.StartListening(5000, 1);
                    _currentPosition = Geolocator.GetPositionAsync(1000).Result;
//                    Geolocator.StopListening();
                }
                return _currentPosition;
            }
        }

        static GeolocationUtility()
        {
            Geolocator = DependencyService.Get<IGeolocator>();
        }
    }
}