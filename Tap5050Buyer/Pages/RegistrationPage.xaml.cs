using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Tap5050Buyer
{
    public partial class RegistrationPage : ContentPage
    {
        private AccountInfoViewModel _viewModel;

        public List<string> c_contactMethods = new List<string>() { "Do not contact me", "SMS", "Email" };

        public RegistrationPage()
        {
            InitializeComponent();
            Title = "Registration";
            _tableView.Intent = TableIntent.Menu;

            _viewModel = new AccountInfoViewModel();
            _viewModel.UserAccount = new UserAccount(); // Hack !!

            this.BindingContext = _viewModel;

            var raffleResultsViewCell = new ViewCell();
            _raffleResultsSection.Add(raffleResultsViewCell);

            var raffleResultsLayout = new StackLayout
            {
                Padding = new Thickness(10, 0, 10, 0),
            };
            raffleResultsViewCell.View = raffleResultsLayout;

            var raffleResultsPicker = new Picker
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };
            raffleResultsLayout.Children.Add(raffleResultsPicker);

            foreach (var item in c_contactMethods)
            {
                raffleResultsPicker.Items.Add(item);
            }
            raffleResultsPicker.SetBinding(Picker.SelectedIndexProperty, new Binding("UserAccount.PreferedContactMethod", BindingMode.TwoWay, new PickerContactMethodsConverter()));

            var charityMessagesViewCell = new ViewCell();
            _charityMessagesSection.Add(charityMessagesViewCell);

            var charityMessagesLayout = new StackLayout
            {
                Padding = new Thickness(10, 0, 10, 0),
            };
            charityMessagesViewCell.View = charityMessagesLayout;

            var charityMessagesPicker = new Picker
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };
            charityMessagesLayout.Children.Add(charityMessagesPicker);

            foreach (var item in c_contactMethods)
            {
                charityMessagesPicker.Items.Add(item);
            }
            charityMessagesPicker.SetBinding(Picker.SelectedIndexProperty, new Binding("UserAccount.PreferedContactMethodcharity", BindingMode.TwoWay, new PickerContactMethodsConverter()));

            var createAccountButtonViewCell = new ViewCell();
            _createAccountButtonSection.Add(createAccountButtonViewCell);

            var createAccountButtonLayout = new StackLayout
            {
                Padding = new Thickness(5, 0, 5, 0),
                BackgroundColor = Color.Accent,
            };
            createAccountButtonViewCell.View = createAccountButtonLayout;

            var createAccountButton = new Button
            {
                Text = "Create Account",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                TextColor = Color.White,
            };
            createAccountButtonLayout.Children.Add(createAccountButton);

            createAccountButton.Clicked += (sender, e) =>
            {
                _viewModel.CreateAccount();
            };
        }
    }
}

