using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Tap5050Buyer
{
    public partial class RaffleListPage : ContentPage
    {
        public RaffleListPage(bool locationDetected, IList<string> raffleLocations, GeonamesCountrySubdivision countrySubdivision)
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);

            var locationPicker = new Picker();
            locationPicker.HorizontalOptions = LayoutOptions.Center;
            locationPicker.Title = "Pick a province";
            layout.Children.Add(locationPicker);

            if (locationDetected)
            {
                locationPicker.Items.Add(countrySubdivision.AdminName);
                locationPicker.SelectedIndex = 0;
                locationPicker.IsEnabled = false;

                // Fetch raffle list then populate

                var raffleEvents = GetRaffleEvents();

                var raffleEventListView = new ListView();
                raffleEventListView.ItemsSource = raffleEvents;
                raffleEventListView.ItemTemplate = new DataTemplate(typeof(RaffleEventCell));
                raffleEventListView.ItemSelected += (sender, e) =>
                {
                    if (e.SelectedItem != null)
                    {
                        this.Navigation.PushAsync(new RaffleDetailsPage(raffleEvents));
                        raffleEventListView.SelectedItem = null;
                    }
                };
                layout.Children.Add(raffleEventListView);
            }
            else
            {
                foreach (var location in raffleLocations)
                {
                    locationPicker.Items.Add(location);
                }
                locationPicker.IsEnabled = true;
            }

        }

        public List<RaffleEvent> GetRaffleEvents()
        {
            return new List<RaffleEvent>
            {
                new RaffleEvent
                {
                    Name = "Chris Time Zone Event",
                    Organization = "Main Testing Organization",
                    Description = "Put description here for event 12502",
                    ImageUrl = "http://i.imgur.com/ostrOHz.png",
                },
                new RaffleEvent
                {
                    Name = "Jay Test Event Large Raffle",
                    Organization = "Main Testing Organization",
                    Description = "Foo Bar",
                    ImageUrl = "http://i.imgur.com/ostrOHz.png",
                }
            };
        }
    }

    public class RaffleEventCell : ViewCell
    {
        public RaffleEventCell()
        {
            var viewLayout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Orientation = StackOrientation.Horizontal,
            };
            
            var image = new Image
            {
                WidthRequest = 44,
                HeightRequest = 44,
            };
            image.SetBinding(Image.SourceProperty, new Binding("ImageUrl"));

            var labelLayout = new StackLayout
            {
                Padding = new Thickness(5, 0, 0, 0),
                VerticalOptions = LayoutOptions.StartAndExpand,
                Orientation = StackOrientation.Vertical,
            };
            
            var eventNameLabel = new Label
            {
                YAlign = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            };
            eventNameLabel.SetBinding(Label.TextProperty, new Binding("Name"));

            var organizationNameLable = new Label
            {
                YAlign = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)), 
            };
            organizationNameLable.SetBinding(Label.TextProperty, new Binding("Organization"));

            labelLayout.Children.Add(eventNameLabel);
            labelLayout.Children.Add(organizationNameLable);

            viewLayout.Children.Add(image);
            viewLayout.Children.Add(labelLayout);

            View = viewLayout;
        }
    }
}