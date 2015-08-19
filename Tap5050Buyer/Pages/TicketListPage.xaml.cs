using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Tap5050Buyer
{
    public partial class TicketListPage : ContentPage
    {
        private TicketListViewModel _viewModel;

        public TicketListPage()
        {
            InitializeComponent();
            this.Title = "Your Raffles";

            if (DatabaseManager.Token == null)
            {
                var layout = new StackLayout
                {
                    Children =
                    { new Label
                        {
                            Text = "Please login to retrieve your tickets.",
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                        }
                    },
                    Padding = new Thickness(20, 0, 20, 0),
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                };

                this.Content = layout;
            }
            else
            {
                _viewModel = new TicketListViewModel();
                this.BindingContext = _viewModel;

                GetTicketsAndCreateList();
            }
        }

        public async void GetTicketsAndCreateList()
        {
            await _viewModel.LoadData();
            CreateList();
        }

        public void CreateList()
        {
            var layout = new StackLayout();

            if ((_viewModel.UserAccount != null) & (_viewModel.UserAccount.MobilePhoneVerified == "N"))
            {
                var verifyButtonLayout = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Padding = new Thickness(20, 5, 20, 5),
                };
                layout.Children.Add(verifyButtonLayout);

                var label = new Label
                {
                    Text = "To Display 50/50's Verify Phone",
                    HorizontalOptions = LayoutOptions.Center,
                };
                verifyButtonLayout.Children.Add(label);

                var button = new Button
                {
                    Text = "Verify",
                    HorizontalOptions = LayoutOptions.Center,
                };
                verifyButtonLayout.Children.Add(button);
                button.Clicked += (sender, e) =>
                {
                    // Not very nice here!!
                    this.Navigation.PushAsync(new VerifyPhonePage(_viewModel.UserAccount.Email, _viewModel.UserAccount.Phone, _viewModel.UserAccount.CountryCode));
                };
            }
           
            var ticketsListView = new ListView();
            layout.Children.Add(ticketsListView);

            ticketsListView.SetBinding(ListView.ItemsSourceProperty, "EventForTicketsList", BindingMode.TwoWay);
            ticketsListView.ItemTemplate = new DataTemplate(typeof(TicketCell));
            ticketsListView.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem != null)
                {
                    // PushAsync a new RaffleDetailsPage instead of creating one and reuse it: to workaround a bug in Carousel + TabbedPage in iOS
                    this.Navigation.PushAsync(new TicketDetailPage(_viewModel.Tickets, ((RaffleEventForTickets)e.SelectedItem).Id));
                    ticketsListView.SelectedItem = null;
                }
            };

            this.Content = layout;
        }
    }

    public class TicketCell : ViewCell
    {
        public TicketCell()
        {
            var viewLayout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Orientation = StackOrientation.Horizontal,
            };
            View = viewLayout;

            var image = new Image
            {
                WidthRequest = 44,
                HeightRequest = 44,
            };
            image.SetBinding(Image.SourceProperty, new Binding("ImageUrl"));
            viewLayout.Children.Add(image);

            var labelLayout = new StackLayout
            {
                Padding = new Thickness(5, 0, 0, 0),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Orientation = StackOrientation.Vertical,
            };
            viewLayout.Children.Add(labelLayout);

            var raffleNameLabel = new Label
            {
                YAlign = TextAlignment.Center,
                LineBreakMode = LineBreakMode.TailTruncation,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            };
            raffleNameLabel.SetBinding(Label.TextProperty, new Binding("Name"));
            labelLayout.Children.Add(raffleNameLabel);
        }
    }
}