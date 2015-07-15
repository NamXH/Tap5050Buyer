using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Tap5050Buyer
{
    public partial class AccountInfoPage : ContentPage
    {
        private AccountInfoViewModel _viewModel;

        public AccountInfoPage(AccountInfoViewModel vm)
        {
            InitializeComponent();
            Title = "Account Info";
            _tableView.Intent = TableIntent.Menu;

            _viewModel = vm; // A hack!! Need to put loading screen before instead.
            this.BindingContext = _viewModel;
        }
    }
}