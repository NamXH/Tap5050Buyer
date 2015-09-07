using System;
using Xamarin.Forms;

namespace Tap5050Buyer
{
    public class RaffleNavigationPage : NavigationPage
    {
        public RaffleNavigationPage()
        {
            Title = "Raffle";
            Icon = "cart.png";
            this.PushAsync(new RaffleListPage(LoadingLocationViewModel.IsLocationDetected, LoadingLocationViewModel.RaffleLocations, LoadingLocationViewModel.UserSelectedLocation, GeolocationManager.CountrySubdivision));
        }
    }
}