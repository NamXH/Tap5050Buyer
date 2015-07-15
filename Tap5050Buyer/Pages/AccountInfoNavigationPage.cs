using System;
using Xamarin.Forms;

namespace Tap5050Buyer
{
    public class AccountInfoNavigationPage : NavigationPage
    {
        public AccountInfoNavigationPage()
        {
            Title = "Account Info";
            this.PushAsync(new LoginPage());
        }
    }
}