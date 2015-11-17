using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Tap5050Buyer
{
    public partial class TicketListPage : ContentPage
    {
        internal const string c_loadingMessage = "Loading your tickets.";

        private TicketListViewModel _viewModel;

        private StackLayout _layout;

        private StackLayout _verifyButtonLayout;

        public TicketListPage()
        {
            InitializeComponent();
            this.Title = "Your Raffles";
            NavigationPage.SetBackButtonTitle(this, "Back");

            if (DatabaseManager.Token == null)
            {
                var warningLayout = new StackLayout
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
                
                this.Content = warningLayout;
            }
            else
            {
                _viewModel = new TicketListViewModel();
                this.BindingContext = _viewModel;

                GetTicketsAndCreateList();

                MessagingCenter.Subscribe<VerifyPhonePage>(this, "Verified", (sender) =>
                    {
                        if (_verifyButtonLayout != null)
                        {
                            _layout.Children.Remove(_verifyButtonLayout);
                        }
                    });
            }
        }

        public async void GetTicketsAndCreateList()
        {
            // Show indicator while loading (can be refactored to be cleaner!!)
            _layout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Padding = new Thickness(20, 0, 20, 0),
            };
            _layout.Children.Add(new ActivityIndicator
                {
                    IsRunning = true
                });
            _layout.Children.Add(new Label
                {
                    Text = c_loadingMessage
                });
            this.Content = _layout;

            await _viewModel.LoadData(); // Can reduce app startup time by loading this data only when the Tickets tab is selected !!
            CreateList();
        }

        public void CreateList()
        {
            _layout = new StackLayout();

            if ((_viewModel.UserAccount != null) && (_viewModel.UserAccount.MobilePhoneVerified == "N"))
            {
                _verifyButtonLayout = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Padding = new Thickness(20, 5, 20, 5),
                };
                _layout.Children.Add(_verifyButtonLayout);

                var label = new Label
                {
                    Text = "To Display 50/50's Verify Phone",
                    HorizontalOptions = LayoutOptions.Center,
                };
                _verifyButtonLayout.Children.Add(label);

                var button = new Button
                {
                    Text = "Verify",
                    HorizontalOptions = LayoutOptions.Center,
                };
                _verifyButtonLayout.Children.Add(button);
                button.Clicked += (sender, e) =>
                {
                    // Not very nice here!!
                    this.Navigation.PushAsync(new VerifyPhonePage(_viewModel.UserAccount.Email, _viewModel.UserAccount.Phone, _viewModel.UserAccount.CountryCode));
                };
            }
           
            var ticketsListView = new ListView();
            _layout.Children.Add(ticketsListView);

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

            this.Content = _layout;
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