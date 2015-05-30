using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Tap5050Buyer
{
    public partial class RaffleListPage : ContentPage
    {
        public const string c_serverBaseAddress = "http://dev.tap5050.com/";
        public const string c_serverEventApiAddress = "apex/tap5050_dev/Mobile_Web_Serv/events";

        public RaffleListPage(bool locationDetected, IList<RaffleLocation> raffleLocations, GeonamesCountrySubdivision countrySubdivision)
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

                var raffleLocation = raffleLocations.FirstOrDefault(x => x.Name == countrySubdivision.AdminName);

                if (raffleLocation == null)
                {
                    layout.Children.Add(new Label
                        {
                            Text = "Sorry. There is no available raffle at your location.",
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                        });
                }
                else
                {
                    PopulateRaffleEventList(raffleLocation);
                }
            }
            else
            {
                foreach (var location in raffleLocations)
                {
                    locationPicker.Items.Add(location.Name);
                }
                locationPicker.IsEnabled = true;
            }

        }

        public async void PopulateRaffleEventList(RaffleLocation raffleLocation)
        {
            var raffleEvents = await GetRaffleEventsAtLocation(raffleLocation.Id);

            var raffleEventListView = new ListView();
            raffleEventListView.ItemsSource = raffleEvents;
            raffleEventListView.ItemTemplate = new DataTemplate(typeof(RaffleEventCell));
            raffleEventListView.ItemSelected += (sender, e) =>
                {
                    if (e.SelectedItem != null)
                    {
                        this.Navigation.PushAsync(new RaffleDetailsPage(raffleEvents, ((RaffleEvent)e.SelectedItem).Id));
                        raffleEventListView.SelectedItem = null;
                    }
                };
            layout.Children.Add(raffleEventListView);
        }

        public async Task<List<RaffleEvent>> GetRaffleEventsAtLocation(string locationId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(c_serverBaseAddress);

            HttpResponseMessage response = null;
            try
            {
                response = await client.GetAsync(c_serverEventApiAddress + "?location_id=" + locationId);
            }
            catch (Exception)
            {
                return null;
            }
            var json = response.Content.ReadAsStringAsync().Result;

            var obj = JsonConvert.DeserializeObject<JObject>(json);
            var items = obj["items"];
            if (items != null)
            {
                return JsonConvert.DeserializeObject<List<RaffleEvent>>(items.ToString());
            }
            return null; 
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