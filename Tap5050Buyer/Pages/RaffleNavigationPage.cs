﻿using System;
using Xamarin.Forms;

namespace Tap5050Buyer
{
    public class RaffleNavigationPage : NavigationPage
    {
        public RaffleNavigationPage()
        {
            Title = "Buy";
            NavigationPage.SetBackButtonTitle(this, "Back");
            Icon = "cart.png";
            this.PushAsync(new RaffleListPage(LoadingLocationViewModel.IsLocationDetected, LoadingLocationViewModel.RaffleLocations, LoadingLocationViewModel.UserSelectedLocation, GeolocationManager.CountrySubdivision));
        }
    }
}