using System;
using System.Collections.Generic;

using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace Tap5050Buyer
{
    public partial class LoginPage : ContentPage
    {
        private LoginPageViewModel _viewModel;

        public LoginPage()
        {
            InitializeComponent();
            Title = "Login to Your Account";
            NavigationPage.SetBackButtonTitle(this, "Back");

            _viewModel = new LoginPageViewModel();
            this.BindingContext = _viewModel;

            var tableView = new TableView
            {
                Intent = TableIntent.Menu,
            };
            this.Content = tableView;

            var usernameSection = new TableSection();
            tableView.Root.Add(usernameSection);

            var usernameCell = new EntryCell
            {
                Label = "Login",
                Placeholder = "username",
                Keyboard = Keyboard.Email,
            };
            usernameSection.Add(usernameCell);
            usernameCell.SetBinding(EntryCell.TextProperty, "Username");

//            var passwordCell = new EntryCell
//            {
//                Label = "Password",
//                Placeholder = "password",
//                Keyboard = Keyboard.Text,
//            };
            var passwordCell = new ExtendedEntryCell
            {
                Label = "Password",
                Placeholder = "password",
                Keyboard = Keyboard.Text,
                IsPassword = true,
            };
            usernameSection.Add(passwordCell);
            passwordCell.SetBinding(EntryCell.TextProperty, "Password");

            var forgotPasswordViewCell = new ViewCell();
            usernameSection.Add(forgotPasswordViewCell);

            var forgotPasswordLayout = new StackLayout
            { 
                Padding = new Thickness(5, 0, 5, 0),
            };
            forgotPasswordViewCell.View = forgotPasswordLayout;

            var forgotPasswordButton = new Button
            {
                Text = "Forgot your Password?",
                HorizontalOptions = LayoutOptions.End,
            };
            forgotPasswordLayout.Children.Add(forgotPasswordButton);
            forgotPasswordButton.Clicked += async (sender, e) =>
            {
                ResetPassword(usernameCell.Text);
            };

            var loginSection = new TableSection();
            tableView.Root.Add(loginSection);

            var loginButtonViewCell = new ViewCell();
            loginSection.Add(loginButtonViewCell);
            loginButtonViewCell.Tapped += async (sender, e) =>
            {
                await Login(usernameCell.Text, passwordCell.Text);
            };

            var loginButtonLayout = new StackLayout
            {
                Padding = new Thickness(5, 0, 5, 0),
                BackgroundColor = Color.Accent,
            };
            loginButtonViewCell.View = loginButtonLayout;

            var loginButton = new Button
            {
                Text = "Login",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                TextColor = Color.White,
            };
            loginButtonLayout.Children.Add(loginButton);
            loginButton.Clicked += async (sender, e) =>
            {
                // Maybe just make the call to be Login() and determine the parameters in the VM is better!!
                await Login(usernameCell.Text, passwordCell.Text);
            };

            var signUpSection = new TableSection();
            tableView.Root.Add(signUpSection);

            var signUpCell = new ViewCell();
            signUpSection.Add(signUpCell);

            var signUpLayout = new StackLayout
            {
                Padding = new Thickness(5, 0, 5, 0),
            };
            signUpCell.View = signUpLayout;

            var signUpButton = new Button
            {
                Text = "Don't have an account yet? Sign up",
            };
            signUpLayout.Children.Add(signUpButton);
            signUpButton.Clicked += (sender, e) =>
            {
                this.Navigation.PushAsync(new RegistrationPage(false, this));
            };
        }

        private async Task Login(string username, string password)
        {
            if (String.IsNullOrWhiteSpace(username) || String.IsNullOrWhiteSpace(password))
            {
                DisplayAlert("Validation Error", "Username and Password are required", "Retry");
            }
            else
            {
                var loginResult = await _viewModel.Login();
                if (loginResult.Item1)
                {
                    this.Navigation.PushAsync(new AccountInfoPage());
                    this.Navigation.RemovePage(this);
                }
                else
                {
                    DisplayAlert("Error", loginResult.Item2, "Retry"); 
                }
            } 
        }

        private async Task ResetPassword(string username)
        {
            if (String.IsNullOrWhiteSpace(username))
            {
                DisplayAlert("Validation Error", "Username is required", "Retry"); 
            }
            else
            {
                var resetPasswordResult = await _viewModel.ResetPassword(username);
                if (resetPasswordResult)
                {
                    DisplayAlert("Success", "A temporary password has been sent to your registered email.", "OK");
                }
                else
                {
                    DisplayAlert("Error", "Server request error.", "Retry"); 
                }
            }
        }
    }
}