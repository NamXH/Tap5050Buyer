using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Tap5050Buyer
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            Title = "Buy Tickets v1.17";
            NavigationPage.SetBackButtonTitle(this, "Back");

            var scroll = new ScrollView();
            Content = scroll;

            var layout = new StackLayout
            {
                Padding = new Thickness(10, 5, 10, 5), 
            };
            scroll.Content = layout;

            var frame = new Frame
            {
                OutlineColor = Color.Silver,
                HasShadow = true,
            };
            layout.Children.Add(frame);

            var innerLayout = new StackLayout();
            frame.Content = innerLayout;

            var starterLabel = new Label
            {
                Text = "A message from your Tap50:50 team:",
            };
            innerLayout.Children.Add(starterLabel);

            var thankyouLabel = new Label
            {
                Text = "Thank you for supporting your local charities by downloading our app. We work hard to try and put usable technology into the hands of charities to promote their causes and hopefully have some fun doing so. Most apps have a means of promoting their app to your friends and family. We decided it was more important for you to promote the worthy causes the Tap50:50 raffle app is trying to help so we have included links to promote a charity on your favorite social media site. We hope you do so.",
            };
            innerLayout.Children.Add(thankyouLabel);

            var immediateLabel = new Label
            {
                Text = "Things you should know:",
            };
            innerLayout.Children.Add(immediateLabel);

            var thing01 = new Label
            {
                Text = "- We have to geolocate where you are because it is unlawful to purchase tickets from a different jurisdiction. We do this when you open the app so you can see your local raffles. We also do this during the ticket purchase process.",
            };
            innerLayout.Children.Add(thing01);

            var thing02 = new Label
            {
                Text = "- If you have purchased 50/50 tickets on our raffle system at an event, you can see those tickets if you used your mobile phone number at ticket purchase.",
            };
            innerLayout.Children.Add(thing02);

            var thing03 = new Label
            {
                Text = "- You need to verify your mobile phone number to see the 50/50 tickets.",
            };
            innerLayout.Children.Add(thing03);

            var thing04 = new Label
            {
                Text = "- You can only see back 10 days from time of verification of your mobile phone number. This will also allow you to see the winning numbers of raffles you participated in.",
            };
            innerLayout.Children.Add(thing04);

            var disclaimer01 = new Label
            {
                Text = "Disclaimer:",
            };
            innerLayout.Children.Add(disclaimer01);

            var disclaimer02 = new Label
            {
                Text = "Every raffle and every jurisdiction has different rules for their raffles. It is not the intent of this app to replace physical tickets. It is almost certain that in your jurisdiction you will need your physical ticket to claim the prize for basic 50/50 tickets. For raffle tickets that you have given your name and address for it is still a good idea to keep the ticket however the raffle may not dictate this is necessary. In the case where the app conflicts with official raffle results, official raffle results will be deemed to be correct.",
            };
            innerLayout.Children.Add(disclaimer02);

            var goodluckLabel = new Label
            {
                Text = "Good luck and Thanks once again for supporting worthy causes.",
            };
            innerLayout.Children.Add(goodluckLabel);

            var contactLabel = new Label
            {
                Text = "To contact us: info@tap5050.com",
            };
            innerLayout.Children.Add(contactLabel);
        }
    }
}

