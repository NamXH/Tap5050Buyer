using System;
using Xamarin.Forms;
using System.Linq;

namespace Tap5050Buyer
{
    public class TicketsNavigationPage : NavigationPage
    {
        public TicketsNavigationPage()
        {
            Title = "Tickets";
            Icon = "tickets.png";

            this.PushAsync(new TicketListPage());
        }

        // Old implementation that doesn't work. Sometimes, PushAsync/PopAsync... doesn't update NavigationStack.Count
//        private void Refresh()
//        {
//            var oldRoot = this.Navigation.NavigationStack.First();
//            this.Navigation.InsertPageBefore(new TicketListPage(), oldRoot);
//            this.Navigation.PopAsync();
//
////            this.PushAsync(new TicketListPage(), false);
////
////            if (this.Navigation.NavigationStack.Count() == 1)
////            {
////            }
////            else
////            {
////                this.Navigation.RemovePage(oldRoot); 
////            }
//        }
    }
}

