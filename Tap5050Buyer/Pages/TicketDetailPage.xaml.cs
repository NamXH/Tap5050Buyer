using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Tap5050Buyer
{
    public partial class TicketDetailPage : CarouselPage
    {
        private TicketDetailViewModel _viewModel;

        public TicketDetailPage(List<Ticket> tickets, int eventId)
        {
            InitializeComponent();
            Title = "Your Tickets";

            _viewModel = new TicketDetailViewModel(tickets);

            LoadDataThenCreateCarouselPage(eventId);
        }

        public async void LoadDataThenCreateCarouselPage(int eventId)
        {
            await _viewModel.LoadData();

            foreach (var ticketGroup in _viewModel.TicketGroups)
            {
                var page = CreatePage(ticketGroup);
                Children.Add(page);
                if (ticketGroup.EventId == eventId)
                {
                    this.SelectedItem = page;
                } 
            }
        }

        public ContentPage CreatePage(TicketGroup ticketGroup)
        {
            var page = new ContentPage();

            var scroll = new ScrollView();
            page.Content = scroll;

            var layout = new StackLayout
            {
                Padding = new Thickness(10, 5, 10, 5),
            };
            scroll.Content = layout;

            var firstTicket = ticketGroup.Tickets.FirstOrDefault();
            if (firstTicket == null)
            {
                return page;
            }

            #region Image
            var imageLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
            };
            layout.Children.Add(imageLayout);

            var image = new Image
            {
                WidthRequest = 44,
                HeightRequest = 44,
                Source = firstTicket.ImageUrl,
            };
            imageLayout.Children.Add(image);

            var raffleName = new Label
            {
                Text = firstTicket.RaffleName,
                HorizontalOptions = LayoutOptions.Center,
            };
            imageLayout.Children.Add(raffleName);
            #endregion

            #region Prize
            var prizeDescription = new Label
            {
                Text = firstTicket.PrizeDescription,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                TextColor = Color.Green,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            layout.Children.Add(prizeDescription);

            var jackpotTotal = new Label
            {
                Text = "$" + firstTicket.JackpotTotal,
                TextColor = Color.Red,
                FontSize = 35,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            layout.Children.Add(jackpotTotal);
            #endregion

            var labelWidthRequest = 128.00;

            foreach (var ticket in ticketGroup.Tickets)
            {
                var frame = new Frame
                {
                    OutlineColor = Color.Silver,
                    HasShadow = true,
                };
                layout.Children.Add(frame);

                var ticketLayout = new StackLayout();
                frame.Content = ticketLayout;

                #region Draw Date
                var drawDateLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                };
                ticketLayout.Children.Add(drawDateLayout);

                var drawDateLabel = new Label
                {
                    Text = "Draw Date:",
                    FontAttributes = FontAttributes.Bold,
                    WidthRequest = labelWidthRequest,
                    XAlign = TextAlignment.End,
                };
                drawDateLayout.Children.Add(drawDateLabel);

                var date = new Label();
                drawDateLayout.Children.Add(date);
                date.Text = ticket.DrawDate.ToString("MMM dd, yyyy");
                #endregion

                #region Licence
                var licenceLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                };
                ticketLayout.Children.Add(licenceLayout);

                var licenceLabel = new Label
                {
                    Text = "Lottery License:",
                    FontAttributes = FontAttributes.Bold,
                    WidthRequest = labelWidthRequest,
                    XAlign = TextAlignment.End,
                };
                licenceLayout.Children.Add(licenceLabel);

                var licence = new Label();
                licenceLayout.Children.Add(licence);
                licence.Text = ticket.LicenceNumber;
                #endregion

                #region Winning Numbers
                var winningNumbersLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                };
                ticketLayout.Children.Add(winningNumbersLayout);

                var winningNumbersLabel = new Label
                {
                    Text = "Winning #(s):",
                    FontAttributes = FontAttributes.Bold,
                    WidthRequest = labelWidthRequest,
                    XAlign = TextAlignment.End,
                };
                winningNumbersLayout.Children.Add(winningNumbersLabel);

                var numbersSubLayout = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                };
                winningNumbersLayout.Children.Add(numbersSubLayout);

                if (ticket.WinningNumbers != null)
                {
                    foreach (var number in ticket.WinningNumbers)
                    {
                        var numberLabel = new Label
                        {
                            Text = number.Number,
                        };
                        numbersSubLayout.Children.Add(numberLabel);
                    }
                }
                #endregion

                var gap = new Label
                {
                    Text = "",
                    HeightRequest = 10.0,
                };
                ticketLayout.Children.Add(gap);

                #region Ticket Number
                var ticketNumberLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                };
                ticketLayout.Children.Add(ticketNumberLayout);

                var ticketNumberLabel = new Label
                {
                    Text = "My Number(s):",
                    FontAttributes = FontAttributes.Bold,
                    WidthRequest = labelWidthRequest,
                    XAlign = TextAlignment.End,
                };
                ticketNumberLayout.Children.Add(ticketNumberLabel);

                var numbersLayout = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                };
                ticketNumberLayout.Children.Add(numbersLayout);

                if (ticket.TicketNumbers != null)
                {
                    foreach (var number in ticket.TicketNumbers)
                    {
                        var numberLabel = new Label
                        {
                            Text = number.Number,
                        };
                        numbersLayout.Children.Add(numberLabel);
                    }
                }
                #endregion
            }

            var raffleStatusLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
            };
            layout.Children.Add(raffleStatusLayout);

            var raffleText = new Label
            {
                Text = "Raffle is ",
                VerticalOptions = LayoutOptions.Center,
            };
            raffleStatusLayout.Children.Add(raffleText);

            if (firstTicket.RaffleStatus == "Y")
            {
                var status = new Label
                {
                    Text = "OPEN",
                    TextColor = Color.Green,
                    VerticalOptions = LayoutOptions.Center,
                };
                raffleStatusLayout.Children.Add(status);

                if (firstTicket.RaffleType == "Large")
                {
                    var button = new Button
                    {
                        Text = "Buy More Tickets",
                        VerticalOptions = LayoutOptions.Center,
                    };
                    raffleStatusLayout.Children.Add(button);

                    button.Clicked += (sender, e) =>
                    {
                        var browser = new WebView();
                        browser.Source = firstTicket.BuyTicketUrl;

                        var browserPage = new ContentPage();
                        browserPage.Content = browser;
                        browserPage.Title = "Buy Tickets";

                        this.Navigation.PushAsync(browserPage);
                    };
                }
            }
            else
            {
                var status = new Label
                {
                    Text = "OVER",
                    TextColor = Color.Red,
                    VerticalOptions = LayoutOptions.Center,
                };
                raffleStatusLayout.Children.Add(status); 
            }

            #region Disclaimer
            var disclaimerFrame = new Frame
            {
                OutlineColor = Color.Silver,
                HasShadow = true,
            };
            layout.Children.Add(disclaimerFrame);

            var disclaimerLayout = new StackLayout();
            disclaimerFrame.Content = disclaimerLayout;

            var disclaimerLabel = new Label
            {
                Text = "Tap50:50 believes the information posted here is accurate and correct. In the case where the information conflicts with official results, the official results will be deemed correct.",
            };
            disclaimerLayout.Children.Add(disclaimerLabel);

            var warningLabel1 = new Label
            {
                TextColor = Color.Red,
                Text = "Warning:",
            };
            disclaimerLayout.Children.Add(warningLabel1);

            var warningLabel2 = new Label
            {
                Text = "Do not throw away your basic tickets that do not contain your name (usually 50/50 tickets). The physical ticket is required to qualify to win.",
            };
            disclaimerLayout.Children.Add(warningLabel2);
            #endregion

            return page;
        }
    }
}

