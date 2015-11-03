using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Linq;

namespace Tap5050Buyer
{
    public partial class SlaveEventsPage : ContentPage
    {
        public SlaveEventsPage(bool isLocationDetected, IEnumerable<RaffleEvent> raffleEvents)
        {
            InitializeComponent();
            Title = "Choose Charity";
            NavigationPage.SetBackButtonTitle(this, "Back");

            if ((raffleEvents != null) && (raffleEvents.Any()))
            {
                var raffleEventListView = new ListView();
                raffleEventListView.ItemsSource = raffleEvents;
                raffleEventListView.ItemTemplate = new DataTemplate(typeof(RaffleEventCell));
                raffleEventListView.ItemSelected += (sender, e) =>
                {
                    if (e.SelectedItem != null)
                    {
                        var selectedEvent = (RaffleEvent)e.SelectedItem;

                        // PushAsync a new RaffleDetailsPage instead of creating one and reuse it: to workaround a bug in Carousel + TabbedPage in iOS !!
                        this.Navigation.PushAsync(new RaffleDetailsPage(isLocationDetected, raffleEvents, selectedEvent.Id));
                        raffleEventListView.SelectedItem = null;
                    }
                };
                _layout.Children.Add(raffleEventListView);
            }
        }
    }
}

