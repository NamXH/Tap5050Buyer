using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Tap5050Buyer
{
    public partial class TabsPage : TabbedPage 
    {
        public TabsPage()
        {
            InitializeComponent();
            this.SelectedItem = _raffleNavigationPage;

            this.Children.Insert(2, new TicketsNavigationPage());

            MessagingCenter.Subscribe<LoginPageViewModel>(this, "Login", (sender) =>
                {
                    RefreshTicketsTab();
                });

            MessagingCenter.Subscribe<AccountInfoViewModel>(this, "Logout", (sender) =>
                {
                    RefreshTicketsTab();
                });
        }

        public void RefreshTicketsTab()
        {
            this.Children.RemoveAt(2);
            this.Children.Insert(2, new TicketsNavigationPage());
        }
    }
}