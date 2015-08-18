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
                date.Text = ticket.DrawDate.ToString("MMMM dd, yyyy");
                #endregion

                #region Draw Date
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


            }

            return page;
        }
    }
}

