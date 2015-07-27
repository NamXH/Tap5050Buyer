using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Threading.Tasks;

namespace Tap5050Buyer
{
    public partial class TicketsPage : CarouselPage
    {
        private TicketsViewModel _viewModel;

        public TicketsPage()
        {
            InitializeComponent();
            Title = "Tickets";

            // Comment out to test !!
//            if (DatabaseManager.Token == null)
//            {
//                var page = new ContentPage();
//                this.Children.Add(page);
//
//                var layout = new StackLayout
//                {
//                    VerticalOptions = LayoutOptions.CenterAndExpand,
//                };
//                page.Content = layout;
//
//                var label = new Label
//                {
//                    Text = "Please login to see your tickets.",
//                };
//                layout.Children.Add(label);
//            }
//            else
//            {
//                _viewModel = new TicketsViewModel();
//                this.BindingContext = _viewModel;
//                LoadDataAndCreatePages();
//            }
            _viewModel = new TicketsViewModel();
            this.BindingContext = _viewModel;
            LoadDataAndCreatePages();
        }

        public async void LoadDataAndCreatePages()
        {
            await _viewModel.LoadData();

            foreach (var ticket in _viewModel.Tickets)
            {
                var page = CreatePage(ticket);
                this.Children.Add(page);
            }
        }

        public ContentPage CreatePage(Ticket ticket)
        {
            var page = new ContentPage();
            page.BindingContext = ticket;

            var tableView = new TableView
            {
                Intent = TableIntent.Menu,
            };
            page.Content = tableView;

            var raffleNameSection = new TableSection
            {
                Title = "Raffle Name",
            };
            tableView.Root.Add(raffleNameSection);

            var raffleName = new TextCell();
            raffleNameSection.Add(raffleName);
            raffleName.SetBinding(TextCell.TextProperty, "RaffleName");

            var drawDateSection = new TableSection
                {
                    Title = "Draw Date",
                };
            tableView.Root.Add(drawDateSection);

            var drawDate = new TextCell();
            drawDateSection.Add(drawDate);
            drawDate.SetBinding(TextCell.TextProperty, "DrawDate");

            var licenceNumberSection = new TableSection
                {
                    Title = "Licence Number",
                };
            tableView.Root.Add(licenceNumberSection);

            var licenceNumber = new TextCell();
            licenceNumberSection.Add(licenceNumber);
            licenceNumber.SetBinding(TextCell.TextProperty, "LicenceNumber");

            return page;
        }
    }
}

