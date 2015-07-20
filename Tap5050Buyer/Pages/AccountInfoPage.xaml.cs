using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Tap5050Buyer
{
    public partial class AccountInfoPage : ContentPage
    {
        private AccountInfoViewModel _viewModel;

        public AccountInfoPage()
        {
            InitializeComponent();
            Title = "Account Info";

            // If use code instead of xaml, we don't have to remove just add the code after. However, this is good too.
            // Have to remove before setting the binding context because we don't implement Notify Property Changed in the View Model.
            _layout.Children.Remove(_tableView); 

            _viewModel = new AccountInfoViewModel();
            this.BindingContext = _viewModel;

            LoadData();
        }

        private async void LoadData()
        {
            await _viewModel.GetAccountInfo();
            _layout.Children.Remove(_indicator);
            _layout.Children.Add(_tableView);
        }
    }
}