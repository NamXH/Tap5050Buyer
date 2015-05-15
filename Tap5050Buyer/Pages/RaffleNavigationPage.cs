using System;
using Xamarin.Forms;

namespace Tap5050Buyer
{
    public class RaffleNavigationPage : NavigationPage
    {
        public RaffleNavigationPage()
        {
            Title = "Raffle";
            this.PushAsync(new LoadingLocationPage());
        }
    }
}