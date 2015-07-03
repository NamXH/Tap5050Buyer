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

        public ContactsPage(IList<ExtendedContact> contacts, int socialMediaMethod)
        {
            InitializeComponent();

            _viewModel = new ContactsViewModel(contacts);
            BindingContext = _viewModel;

            Title = "Contacts";

            if (socialMediaMethod == 0)
            {
                this.ToolbarItems.Add(new ToolbarItem("Done", null, _viewModel.SendSms));
            }
            else
            {
                this.ToolbarItems.Add(new ToolbarItem("Done", null, _viewModel.SendEmails)); 
            }
        }
    }
}