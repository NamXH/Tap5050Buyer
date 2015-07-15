using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Tap5050Buyer
{
    public partial class LoginPage : ContentPage
    {
        private LoginPageViewModel _viewModel;

        public LoginPage()
        {
            InitializeComponent();
            Title = "Login to Your Account";

            _viewModel = new LoginPageViewModel();
            this.BindingContext = _viewModel;

            var tableView = new TableView
            {
                Intent = TableIntent.Form,
            };
            this.Content = tableView;

            var usernameSection = new TableSection();
            tableView.Root.Add(usernameSection);

            var usernameCell = new EntryCell
            {
                Label = "Login",
                Placeholder = "username",
                Keyboard = Keyboard.Text,
            };
            usernameSection.Add(usernameCell);
            usernameCell.SetBinding(EntryCell.TextProperty, "Username");

            var passwordCell = new EntryCell
            {
                Label = "Password",
                Placeholder = "password",
                Keyboard = Keyboard.Text,
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

            var loginSection = new TableSection();
            tableView.Root.Add(loginSection);

            var loginButtonViewCell = new ViewCell();
            loginSection.Add(loginButtonViewCell);

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
                if (String.IsNullOrWhiteSpace(usernameCell.Text))
                {
                    DisplayAlert("Missing Username", "Username is required", "OK");
                }
                else
                {
                    if (String.IsNullOrWhiteSpace(passwordCell.Text))
                    {
                        DisplayAlert("Missing Password", "Password is required", "OK");
                    }
                    else
                    {
                        var loginResult = await _viewModel.Login();
                            if (loginResult.Item1)
                            {
                                this.Navigation.PushAsync(new AccountInfoPage());
                            }
                            else
                            {
                                DisplayAlert("Error", loginResult.Item2, "OK"); 
                            }
                    }
                }
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
        }
    }
}