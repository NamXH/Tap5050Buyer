using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace Tap5050Buyer
{
    public partial class RaffleDetailsPage : CarouselPage
    {
        public bool LocationDetected
        {
            get;
            set;
        }

        public RaffleDetailsPage(bool locationDetected, IList<RaffleEvent> raffleEvents, int selectedRaffleId)
        {
            InitializeComponent();

            LocationDetected = locationDetected;
            foreach (var raffle in raffleEvents)
            {
                var page = CreateRaffleEventDetailsPage(raffle);
                Children.Add(page);
                if (raffle.Id == selectedRaffleId)
                {
                    this.SelectedItem = page;
                }
            }
        }

        public ContentPage CreateRaffleEventDetailsPage(RaffleEvent raffle)
        {
            var page = new ContentPage();

            var layout = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(20, 5, 20, 5),
            };
            page.Content = layout;

            #region Jackpot
            if (raffle.HasJackpot == "Y")
            {
                var jackpotDescription = new Label
                {
                    Text = raffle.JackpotDescription,
                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                };
                layout.Children.Add(jackpotDescription);

                var jackpotTotal = new Label
                {
//                    Text = "$" + raffle.JackpotTotal,
                    Text = "$1,000,000",
                    TextColor = Color.Red,
                    FontSize = 35,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                };
                layout.Children.Add(jackpotTotal);
            }
            #endregion

            var imageAndNameLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            layout.Children.Add(imageAndNameLayout);

            #region Image and Licence Number
            var imageLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };
            imageAndNameLayout.Children.Add(imageLayout);

            var quarterScreenSize = (DeviceService.Device.Display.Width - layout.Padding.Left - layout.Padding.Right) / 4.5;
            var image = new Image
            {
                Source = ImageSource.FromUri(new Uri(raffle.ImageUrl)),
                WidthRequest = quarterScreenSize,
                HeightRequest = quarterScreenSize,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };
            imageLayout.Children.Add(image);

            if (!String.IsNullOrWhiteSpace(raffle.LicenceNumber))
            {
                var licenceLabel = new Label
                {
                    Text = "Licence # " + raffle.LicenceNumber,
                    FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                };
                imageLayout.Children.Add(licenceLabel);
            }
            #endregion

            #region Name and Organization
            var nameLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };
            imageAndNameLayout.Children.Add(nameLayout);

            var nameLabel = new Label
            {
                Text = raffle.Name,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Center,
            };
            nameLayout.Children.Add(nameLabel);

            var organizationLabel = new Label
            {
                Text = raffle.Organization,
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Center,
            };
            nameLayout.Children.Add(organizationLabel);
            #endregion

            var descriptionLabel = new Label
            {
                Text = raffle.Description,
                HorizontalOptions = LayoutOptions.StartAndExpand,
            };
            layout.Children.Add(descriptionLabel);

            #region Buttons
            var buttonsLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
            };
            layout.Children.Add(buttonsLayout);

            var prizeButton = new Button
            {
                Text = "See Prizes",
                BorderWidth = 1,
                BorderColor = Color.Blue,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            prizeButton.Clicked += (sender, e) =>
            {
//                var browser = new WebView();
//                browser.Source = raffle.PrizeUrl;
//
//                var browserPage = new ContentPage();
//                browserPage.Content = browser;
//                browserPage.Title = "See Prizes";
//
//                this.Navigation.PushAsync(browserPage);

                Device.OpenUri(new Uri(raffle.PrizeUrl)); // External browser
            };
            buttonsLayout.Children.Add(prizeButton);

            var buyButton = new Button
            {
                Text = "Buy Tickets",
                BorderWidth = 1,
                BorderColor = Color.Blue,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            buyButton.Clicked += (sender, e) =>
            {
                var browser = new WebView();
                browser.Source = raffle.BuyTicketUrl;


//                browser.Navigated += async (object obj, WebNavigatedEventArgs eventArgs) =>
//                {
//                    Debug.WriteLine(eventArgs.Url);
//                    var action = await DisplayActionSheet("ActionSheet: Send to?", "Cancel", null, "Email", "Twitter", "Facebook");
//                    Debug.WriteLine("Action: " + action);
//                };

                var browserPage = new ContentPage();
                browserPage.Content = browser;
                browserPage.Title = "Buy Tickets";

                this.Navigation.PushAsync(browserPage);
            };
            buttonsLayout.Children.Add(buyButton);
            #endregion

            if (!LocationDetected)
            {
                var locationWarning = new Label
                {
                    Text = "You may not complete the buy since we cannot detect your location.",
                    FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                    TextColor = Color.Red,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.End,
                };
                layout.Children.Add(locationWarning);
            }

            return page;
        }
    }
}