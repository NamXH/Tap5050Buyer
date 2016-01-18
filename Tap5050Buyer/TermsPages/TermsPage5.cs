using System;

using Xamarin.Forms;

namespace Tap5050Buyer
{
    public class TermsPage5 : ContentPage
    {
        public TermsPage5()
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

            var label09 = new Label
            {
                Text = "SECTION 4 - MODIFICATIONS TO THE SERVICE AND PRICES",
                FontAttributes = FontAttributes.Bold,
            };
            innerLayout.Children.Add(label09);

            var label10 = new Label
            {
                Text = "Prices for our products are subject to change without notice."
                + Environment.NewLine
                + "We reserve the right at any time to modify or discontinue the Service (or any part or content thereof) without notice at any time."
                + Environment.NewLine
                + "We shall not be liable to you or to any third-party for any modification, price change, suspension or discontinuance of the Service.",
            };
            innerLayout.Children.Add(label10);
        }
    }
}


