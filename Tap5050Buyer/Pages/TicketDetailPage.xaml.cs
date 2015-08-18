using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using System.Threading.Tasks;

namespace Tap5050Buyer
{
    public partial class TicketDetailPage : CarouselPage
    {
        private TicketDetailViewModel _viewModel;

        public TicketDetailPage(List<Ticket> Tickets, int eventId)
        {
            InitializeComponent();
            Title = "Your Tickets";

            var a = from ticket in Tickets
                             group ticket by ticket.EventId into g
                             select new { EventId = g.Key, Tickets = g.ToList()};

        }

        public ContentPage CreatePage(Ticket ticket)
        {
            var page = new ContentPage();
            page.BindingContext = ticket;


            return page;
        }
    }
}

