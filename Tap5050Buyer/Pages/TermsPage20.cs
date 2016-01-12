using System;

using Xamarin.Forms;

namespace Tap5050Buyer
{
    public class TermsPage20 : ContentPage
    {
        public TermsPage20()
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

            var label39 = new Label
            {
                Text = "SECTION 19 - CHANGES TO TERMS OF SERVICE",
                FontAttributes = FontAttributes.Bold,
            };
            innerLayout.Children.Add(label39);

            var label40 = new Label
            {
                Text = "You can review the most current version of the Terms of Service at any time at this page."
                + Environment.NewLine
                + "We reserve the right, at our sole discretion, to update, change or replace any part of these Terms of Service by posting updates and changes to our website or app. It is your responsibility to check our website and/or periodically for changes. Your continued use of or access to our website or the Service following the posting of any changes to these Terms of Service constitutes acceptance of those changes.",
            };
            innerLayout.Children.Add(label40);

        }
    }
}


