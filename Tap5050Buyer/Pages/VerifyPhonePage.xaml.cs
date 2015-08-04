using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Tap5050Buyer
{
    public partial class VerifyPhonePage : ContentPage
    {
        private VerifyPhoneNumberViewModel _viewModel;

        public VerifyPhonePage(string email, string phoneNumber, string country)
        {
            InitializeComponent();
            _tableView.Intent = TableIntent.Menu;

            // If use code instead of xaml, we don't have to remove just add the code after. However, this is good too.
            // Have to remove before setting the binding context because we don't implement Notify Property Changed in the View Model.
            _layout.Children.Remove(_tableView); 

            _viewModel = new VerifyPhoneNumberViewModel(email, phoneNumber, country);
            this.BindingContext = _viewModel;

            StartVerificationProcess(email, phoneNumber, country);

            this.ToolbarItems.Add(new ToolbarItem("Done", null, async () =>
                    {
                        var result = await _viewModel.VerifyPhoneNumber();
                        if (result.Item1)
                        {
                            DisplayAlert("Success", "Your phone number has been verified.", "OK");
                        }
                        else
                        {
                            DisplayAlert("Error", result.Item2, "OK"); 
                        }
                    }));
        }

        private async void StartVerificationProcess(string email, string phoneNumber, string country)
        {
            var result = await _viewModel.RequestPhoneNumberVerification();

            _layout.Children.Remove(_indicator);
            if (result.Item1)
            {
                _layout.Children.Add(_tableView); 
            }
            else
            {
                await DisplayAlert("Verification Request Fail", result.Item2, "OK");
                this.Navigation.PopAsync();
            }
        }
    }
}

