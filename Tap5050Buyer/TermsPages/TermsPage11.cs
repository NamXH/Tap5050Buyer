using System;

using Xamarin.Forms;

namespace Tap5050Buyer
{
    public class TermsPage11 : ContentPage
    {
        public TermsPage11()
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

            var label21 = new Label
            {
                Text = "SECTION 10 - PERSONAL INFORMATION",
                FontAttributes = FontAttributes.Bold,
            };
            innerLayout.Children.Add(label21);

            var label22 = new Label
            {
                Text = "Your submission of personal information through the store is governed by our Privacy Policy. To view our Privacy Policy.",
            };
            innerLayout.Children.Add(label22);
        }
    }
}


