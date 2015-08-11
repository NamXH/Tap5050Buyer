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
            this.Title = "Ticket Events";

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
            var ticketsListView = new ListView();
            ticketsListView.SetBinding(ListView.ItemsSourceProperty, "EventForTicketsList", BindingMode.TwoWay);
            ticketsListView.ItemTemplate = new DataTemplate(typeof(TicketCell));
            ticketsListView.ItemSelected += (sender, e) => 
                {
//                    this.Navigation.PushAsync()
                };

            this.Content = ticketsListView;
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
//            image.SetBinding(Image.SourceProperty, new Binding("ImageUrl"));  // Check UI design, do we have image here? !!
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