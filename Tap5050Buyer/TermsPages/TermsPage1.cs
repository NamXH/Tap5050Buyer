using System;

using Xamarin.Forms;

namespace Tap5050Buyer
{
    public class TermsPage1 : ContentPage
    {
        public TermsPage1()
        {
            Title = "Terms of Service";
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

            var label01 = new Label
            {
                Text = "Mobile License Agreement.  Please read this agreement carefully before using the App."
                + Environment.NewLine
                + "OVERVIEW",
                FontAttributes = FontAttributes.Bold,
            };
            innerLayout.Children.Add(label01);

            var label02 = new Label
            {
                Text = "This app is operated by Tap 50:50 Event Consultants Ltd. (Tap 5050). Tap 5050 offers this mobile application, including all information, tools and services available from this app to you, the user, conditioned upon your acceptance of all terms, conditions, policies and notices stated here."
                + Environment.NewLine
                + "By using our app and/ or purchasing/selling something from this app, you engage in our \"Service\" and agree to be bound by the following terms and conditions (\"Terms of Service\", \"Terms\"), including those additional terms and conditions and policies referenced herein and/or available by hyperlink. These Terms of Service apply to all users of the app, including without limitation users who are browsers, vendors, customers, merchants, and/ or contributors of content."
                + Environment.NewLine
                + "Please read these Terms of Service carefully before accessing or using our app. By accessing or using any part of the app, you agree to be bound by these Terms of Service. If you do not agree to all the terms and conditions of this agreement, then you may not access the app or use any services. If these Terms of Service are considered an offer, acceptance is expressly limited to these Terms of Service."
                + Environment.NewLine
                + "Any new features or tools which are added to the current app or online store shall also be subject to the Terms of Service. We reserve the right to update, change or replace any part of these Terms of Service by posting updates. It is your responsibility to check this page periodically for changes. Your continued use of or access to the app following the posting of any changes constitutes acceptance of those changes.",
            };
            innerLayout.Children.Add(label02);
        }
    }
}


