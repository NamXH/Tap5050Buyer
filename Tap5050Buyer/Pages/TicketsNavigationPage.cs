using System;
using Xamarin.Forms;

namespace Tap5050Buyer
{
    public class TicketsNavigationPage : NavigationPage
    {
        public TicketsNavigationPage()
        {
            Title = "Tickets";
            this.PushAsync(new TicketListPage());

            MessagingCenter.Subscribe<LoginPageViewModel>(this, "Login", (sender) =>
                {
                    Refresh();
                });
            
            MessagingCenter.Subscribe<AccountInfoViewModel>(this, "Logout", (sender) =>
                {
                    Refresh();
                });
        }

        private void Refresh()
        {
            this.PopToRootAsync(false);
            var oldRoot = this.CurrentPage;

            this.Navigation.PushAsync(new TicketListPage(), false);
            this.Navigation.RemovePage(oldRoot);
        }
    }
}

