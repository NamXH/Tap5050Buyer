using System;
using Xamarin.Forms;

namespace Tap5050Buyer
{
    public class AboutNavigationPage : NavigationPage
    {
        public AboutNavigationPage()
        {
            Title = "About";
            Icon = "information.png";

            this.PushAsync(new AboutPage());
        }
    }
}

