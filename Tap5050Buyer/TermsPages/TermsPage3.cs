using System;

using Xamarin.Forms;

namespace Tap5050Buyer
{
    public class TermsPage3 : ContentPage
    {
        public TermsPage3()
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

            var label05 = new Label
            {
                Text = "SECTION 2 - GENERAL CONDITIONS",
                FontAttributes = FontAttributes.Bold,
            };
            innerLayout.Children.Add(label05);

            var label06 = new Label
            {
                Text = "We reserve the right to refuse service to anyone for any reason at any time."
                + Environment.NewLine
                + "You understand that your content (not including credit card information), may be transferred unencrypted and involve (a) transmissions over various networks; and (b) changes to conform and adapt to technical requirements of connecting networks or devices. Credit card information is always encrypted during transfer over networks."
                + Environment.NewLine
                + "You agree not to reproduce, duplicate, copy, sell, resell or exploit any portion of the Service, use of the Service, or access to the Service or any contact in the app through which the service is provided, without express written permission by us."
                + Environment.NewLine
                + "The headings used in this agreement are included for convenience only and will not limit or otherwise affect these Terms."
            };
            innerLayout.Children.Add(label06);
        }
    }
}


