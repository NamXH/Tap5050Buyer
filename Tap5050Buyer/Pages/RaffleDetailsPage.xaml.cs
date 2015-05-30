using System;
using System.Collections.Generic;

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
                Padding = new Thickness(20, 0, 20, 0),
            };
            page.Content = layout;

            var image = new Image
            {
                Source = ImageSource.FromUri(new Uri(raffle.ImageUrl)),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = 80,
                HeightRequest = 80,
            };
            layout.Children.Add(image);

            var nameLabel = new Label
            {
                Text = raffle.Name,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.StartAndExpand,
            };
            layout.Children.Add(nameLabel);

            var organizationLabel = new Label
            {
                Text = raffle.Organization,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
            };
            layout.Children.Add(organizationLabel);

            if (raffle.LicenceNumber != null)
            {
                var licenseLabel = new Label
                {
                    Text = "Licence Number: " + raffle.LicenceNumber,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                };
                layout.Children.Add(licenseLabel);
            }

            var descriptionLabel = new Label
            {
                Text = raffle.Description,
                HorizontalOptions = LayoutOptions.StartAndExpand,
            };
            layout.Children.Add(descriptionLabel);

            var jackpotTotal = new Label
            {
                Text = "$" + raffle.JackpotTotal,
                TextColor = Color.Red,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            layout.Children.Add(jackpotTotal);

            var jackpotDescription = new Label
            {
                Text = raffle.JackpotDescription,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
            };
            layout.Children.Add(jackpotDescription);

            var prizeButton = new Button
            {
                Text = "See Prizes",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            prizeButton.Clicked += (sender, e) =>
            {
                var browser = new WebView();
                browser.Source = raffle.PrizeUrl;

                var browserPage = new ContentPage();
                browserPage.Content = browser;
                browserPage.Title = "See Prizes";

                this.Navigation.PushAsync(browserPage);
            };
            layout.Children.Add(prizeButton);

            var buyButton = new Button
            {
                Text = "Buy Tickets",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            buyButton.Clicked += (sender, e) =>
            {
                var browser = new WebView();
                browser.Source = raffle.BuyTicketUrl;

                var browserPage = new ContentPage();
                browserPage.Content = browser;
                browserPage.Title = "Buy Tickets";

                this.Navigation.PushAsync(browserPage);
            };
            layout.Children.Add(buyButton);

            if (!LocationDetected)
            {
                var locationWarning = new Label
                {
                    Text = "You may not complete the buy since we cannot detect your location.",
                    FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.End,
                };
                layout.Children.Add(locationWarning);
            }

            return page;
        }
    }
}