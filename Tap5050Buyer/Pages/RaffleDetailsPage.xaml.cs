using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Tap5050Buyer
{
    public partial class RaffleDetailsPage : CarouselPage
    {
        public RaffleDetailsPage(IList<RaffleEvent> raffleEvents)
        {
            InitializeComponent();
  
            foreach (var raffle in raffleEvents)
            {
                Children.Add(CreateRaffleEventDetailsPage(raffle));
            }
        }

        public ContentPage CreateRaffleEventDetailsPage(RaffleEvent raffle)
        {
            var page = new ContentPage();

            var layout = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand
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
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            layout.Children.Add(nameLabel);

            var descriptionLabel = new Label
            {
                Text = raffle.Description,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            layout.Children.Add(descriptionLabel);

            var buyButton = new Button
            {
                Text = "Buy Tickets",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            buyButton.Clicked += (sender, e) =>
            {
                var browser = new WebView();
                browser.Source = "http://www.tap5050.com/apex/f?p=TICKETBOOTH:PICKTICKET::::APP:P0_EVENT_ID:12502";

                var testPage = new ContentPage();
                testPage.Content = browser;
                testPage.Title = "test";

                this.Navigation.PushAsync(testPage);
            };
            layout.Children.Add(buyButton);

            return page;
        }
    }
}