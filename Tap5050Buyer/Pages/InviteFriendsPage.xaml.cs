using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Tap5050Buyer
{
    public partial class InviteFriendsPage : ContentPage
    {
        public InviteFriendsPage()
        {
            InitializeComponent();

            var browser = new WebView();
            browser.Source = "http://www.tap5050.com/apex/f?p=TICKETBOOTH:PICKTICKET::::APP:P0_EVENT_ID:12502";

            var testPage = new ContentPage();
            testPage.Content = browser;
            testPage.Title = "test";

            var layout = new StackLayout();
            this.Content = layout;
            var button = new Button();
            button.Text = "click me";
            layout.Children.Add(button);
            button.Clicked += (object sender, EventArgs e) => {
                this.Navigation.PushAsync(testPage);
            };
        }
    }
}