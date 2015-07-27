using System;
using Xamarin.Forms;

namespace Tap5050Buyer
{
    public class TicketsNavigationPage : NavigationPage
    {
        public TicketsNavigationPage()
        {
            Title = "Tickets";
            this.PushAsync(new TicketsPage());
        }
    }
}

