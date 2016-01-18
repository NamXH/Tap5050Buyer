using System;

using Xamarin.Forms;

namespace Tap5050Buyer
{
    public class TermsPage12 : ContentPage
    {
        public TermsPage12()
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

            var label23 = new Label
            {
                Text = "SECTION 11 - ERRORS, INACCURACIES AND OMISSIONS",
                FontAttributes = FontAttributes.Bold,
            };
            innerLayout.Children.Add(label23);

            var label24 = new Label
            {
                Text = "Occasionally there may be information in our app or in the Service that contains typographical errors, inaccuracies or omissions that may relate to product descriptions, pricing, promotions, offers, product shipping charges, transit times and availability. We reserve the right to correct any errors, inaccuracies or omissions, and to change or update information or cancel orders if any information in the Service or on any related website is inaccurate at any time without prior notice (including after you have submitted your order)."
                + Environment.NewLine
                + "We undertake no obligation to update, amend or clarify information in the Service or on any related website, including without limitation, pricing information, except as required by law. No specified update or refresh date applied in the Service or on any related website, should be taken to indicate that all information in the Service or on any related website has been modified or updated.",
            };
            innerLayout.Children.Add(label24);
        }
    }
}


