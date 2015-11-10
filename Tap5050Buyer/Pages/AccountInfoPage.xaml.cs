using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Tap5050Buyer
{
    public partial class AccountInfoPage : ContentPage
    {
        private AccountInfoViewModel _viewModel;

        private StackLayout _phoneLayout;

        private Button _verifyButton;

        public AccountInfoPage()
        {
            InitializeComponent();
            Title = "Account Information";
            NavigationPage.SetBackButtonTitle(this, "Back");

            _viewModel = new AccountInfoViewModel();
            this.BindingContext = _viewModel;

            this.ToolbarItems.Add(new ToolbarItem("Edit", null, () =>
                    {
                        this.Navigation.PushAsync(new RegistrationPage(true, this, new UserAccount(_viewModel.UserAccount)));
                    }));

            LoadData();

            MessagingCenter.Subscribe<VerifyPhonePage>(this, "Verified", (sender) =>
                {
                    if (_verifyButton != null)
                    {
                        _phoneLayout.Children.Remove(_verifyButton);
                    }
                });
        }

        private async void LoadData()
        {
            await _viewModel.GetAccountInfo();
            _layout.Children.Remove(_indicator);

            var scroll = new ScrollView();
            _layout.Children.Add(scroll);

            var mainLayout = new StackLayout
            {
                Padding = new Thickness(10, 10, 10, 10),
                Spacing = 10.00,
            };
            scroll.Content = mainLayout;

            #region Player Info
            var playerInfoFrame = new Frame
            {
                OutlineColor = Color.Silver,
                HasShadow = true,
            };
            mainLayout.Children.Add(playerInfoFrame);

            var playerInfoLayout = new StackLayout();
            playerInfoFrame.Content = playerInfoLayout;

            var titleLabel = new Label
            {
                Text = "Player Information",
                HorizontalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            };
            playerInfoLayout.Children.Add(titleLabel);

            var labelWidthRequest = 70.00;

            #region Name
            var nameLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
            };
            playerInfoLayout.Children.Add(nameLayout);

            var nameLabel = new Label
            {
                Text = "Name:",
                FontAttributes = FontAttributes.Bold,
                WidthRequest = labelWidthRequest,
                XAlign = TextAlignment.End,
            };
            nameLayout.Children.Add(nameLabel);

            var name = new Label();
            nameLayout.Children.Add(name);
            name.SetBinding(Label.TextProperty, "UserAccount.FullName");
            #endregion

            #region Address
            var addressLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
            };
            playerInfoLayout.Children.Add(addressLayout);

            var addressLabel = new Label
            {
                Text = "Add:",
                FontAttributes = FontAttributes.Bold,
                WidthRequest = labelWidthRequest,
                XAlign = TextAlignment.End,
            };
            addressLayout.Children.Add(addressLabel);

            var addressMinorLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
            };
            addressLayout.Children.Add(addressMinorLayout);

            var street = new Label();
            addressMinorLayout.Children.Add(street);
            street.SetBinding(Label.TextProperty, "UserAccount.StreetAddress");

            var city = new Label();
            addressMinorLayout.Children.Add(city);
            city.SetBinding(Label.TextProperty, "UserAccount.CityAddress");
            #endregion

            #region Mobile Phone
            _phoneLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
            };
            playerInfoLayout.Children.Add(_phoneLayout);

            var phoneLabel = new Label
            {
                Text = "Mobile:",
                FontAttributes = FontAttributes.Bold,
                WidthRequest = labelWidthRequest,
                XAlign = TextAlignment.End,
                VerticalOptions = LayoutOptions.Center,
            };
            _phoneLayout.Children.Add(phoneLabel);

            var phone = new Label
            { 
                VerticalOptions = LayoutOptions.Center,
            };
            _phoneLayout.Children.Add(phone);
            phone.SetBinding(Label.TextProperty, "UserAccount.Phone");

            if (_viewModel.UserAccount.MobilePhoneVerified == "N")
            {
                _verifyButton = new Button
                {
                    Text = "Verify",
                };
                _phoneLayout.Children.Add(_verifyButton);
                _verifyButton.Clicked += (sender, e) =>
                {
                    StartVerificationProcess();
                };
            }
            #endregion

            #region Email
            var emailLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
            };
            playerInfoLayout.Children.Add(emailLayout);

            var emailLabel = new Label
            {
                Text = "Email:",
                FontAttributes = FontAttributes.Bold,
                WidthRequest = labelWidthRequest,
                XAlign = TextAlignment.End,
            };
            emailLayout.Children.Add(emailLabel);

            var email = new Label();
            emailLayout.Children.Add(email);
            email.SetBinding(Label.TextProperty, "UserAccount.Email");
            #endregion

            #region DOB
            var dobLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
            };
            playerInfoLayout.Children.Add(dobLayout);

            var dobLabel = new Label
            {
                Text = "DOB:",
                FontAttributes = FontAttributes.Bold,
                WidthRequest = labelWidthRequest,
                XAlign = TextAlignment.End,
            };
            dobLayout.Children.Add(dobLabel);

            var dob = new Label();
            dobLayout.Children.Add(dob);
            dob.SetBinding(Label.TextProperty, "UserAccount.BirthdayShortFormat");
            #endregion

            var note = new Label
            {
                Text = "To never miss raffle results verify your phone number.",
                HorizontalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                XAlign = TextAlignment.Center,
            };
            playerInfoLayout.Children.Add(note);
            #endregion

            #region Media
            var mediaFrame = new Frame
            {
                OutlineColor = Color.Silver,
                HasShadow = true,
            };
            mainLayout.Children.Add(mediaFrame);

            var mediaLayout = new StackLayout();
            mediaFrame.Content = mediaLayout;

            var mediaTitle = new Label
            {
                Text = "Spread the Word",
                HorizontalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            };
            mediaLayout.Children.Add(mediaTitle);

            var mediaNote = new Label
            {
                Text = "Let your friends and followers know about your support for your charities by posting automatically on social media.",
                HorizontalOptions = LayoutOptions.Start,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),  
            };
            mediaLayout.Children.Add(mediaNote);
            #endregion

            var buttonLayout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                Orientation = StackOrientation.Horizontal,
            };
            mainLayout.Children.Add(buttonLayout);

            var signOutButton = new Button
            {
                Text = "App Sign Out",
                HorizontalOptions = LayoutOptions.Center,
                BorderColor = Color.Accent,
                BorderWidth = 1,
                BorderRadius = 12,
                HeightRequest = 46,
                WidthRequest = 130,
            };
            buttonLayout.Children.Add(signOutButton);
            signOutButton.Clicked += (sender, e) =>
            {
                _viewModel.SignOut();
                this.Navigation.InsertPageBefore(new LoginPage(), this);
                this.Navigation.PopAsync();
            };

            var resetSocialButton = new Button
            {
                Text = "Reset Social Media",
                HorizontalOptions = LayoutOptions.Center,
                BorderColor = Color.Accent,
                BorderWidth = 1,
                BorderRadius = 12,
                MinimumHeightRequest = 46,
                WidthRequest = 150,
            };
            buttonLayout.Children.Add(resetSocialButton);
            resetSocialButton.Clicked += async (sender, e) =>
            {
                var answer = await DisplayAlert("Confirm", "Reset Facebook and Twitter Login?", "Yes", "No");
                if (answer)
                {
                    DependencyService.Get<ISocialShare>().ResetLogin();
                }
            };
        }

        public async void StartVerificationProcess()
        {
            this.Navigation.PushAsync(new VerifyPhonePage(_viewModel.UserAccount.Email, _viewModel.UserAccount.Phone, _viewModel.UserAccount.CountryCode));
        }
    }
}