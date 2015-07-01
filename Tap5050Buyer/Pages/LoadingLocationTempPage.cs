using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Tap5050Buyer
{
    public class LoadingLocationTempPage : LoadingLocationPage
    {
        public LoadingLocationTempPage() : base()
        {
        }

        public override async void GetCurrentLocationAndRaffleLocationList()
        {
            var updateGeolocationTask = GeolocationManager.UpdateGeolocation();
            var getRaffleLocationsTask = GetRaffleLocations();
            await Task.WhenAll(new List<Task>{ updateGeolocationTask, getRaffleLocationsTask });

            var RaffleLocations = getRaffleLocationsTask.Result;

            if (RaffleLocations == null)
            {
                RemoveAllElement();
                AddTryAgainButton();
            }
            else
            {
                if ((GeolocationManager.CountrySubdivision != null) && (GeolocationManager.CountrySubdivision.AdminName != null))
                {
                    Navigation.PushAsync(new RaffleListPage(true, RaffleLocations, GeolocationManager.CountrySubdivision, true));
                }
                else
                {
                    Navigation.PushAsync(new RaffleListPage(false, RaffleLocations, null, true));
                }
            }
        }
    }
}