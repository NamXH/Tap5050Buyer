using System;
using System.Collections.Generic;
using System.Linq;
using Contacts.Plugin.Abstractions;
using Xamarin.Forms;

namespace Tap5050Buyer
{
    public partial class ContactsPage : ContentPage
    {
        private ContactsViewModel _viewModel;

        public ContactsPage(IList<ExtendedContact> contacts, int socialMediaMethod, RaffleEvent raffle)
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Back");

            _viewModel = new ContactsViewModel(contacts, raffle);
            BindingContext = _viewModel;

            Title = "Contacts";

            if (socialMediaMethod == 0)
            {
                this.ToolbarItems.Add(new ToolbarItem("Next", null, _viewModel.SendSms));
            }
            else
            {
                this.ToolbarItems.Add(new ToolbarItem("Next", null, _viewModel.SendEmails)); 
            }
        }
    }
}