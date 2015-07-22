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

            var verifyButtonSection = new TableSection();
            _tableView.Root.Add(verifyButtonSection);

            var verifyButtonViewCell = new ViewCell();
            verifyButtonSection.Add(verifyButtonViewCell);

            var verifyButtonLayout = new StackLayout
            {
                BackgroundColor = Color.Accent,
            };
            verifyButtonViewCell.View = verifyButtonLayout;

            var verifyButton = new Button
            {
                Text = "Verify Phone Number",
                TextColor = Color.White,
            };
            verifyButtonLayout.Children.Add(verifyButton);

            verifyButton.Clicked += async (sender, e) =>
            {
                StartVerificationProcess(); 
            };
        }

        public async void StartVerificationProcess()
        {
            this.Navigation.PushAsync(new VerifyPhonePage(_viewModel.UserAccount.Email, _viewModel.UserAccount.Phone, _viewModel.UserAccount.Country));
        }
    }
}