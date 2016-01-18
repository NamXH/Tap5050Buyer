using System;

using Xamarin.Forms;

namespace Tap5050Buyer
{
    public class TermsPage6 : ContentPage
    {
        public TermsPage6()
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

            var label11 = new Label
            {
                Text = "SECTION 5 - PRODUCTS OR SERVICES (if applicable)",
                FontAttributes = FontAttributes.Bold,
            };
            innerLayout.Children.Add(label11);

            var label12 = new Label
            {
                Text = "Certain products or services may be available exclusively online through the website. These products or services may have limited quantities and are subject to return or exchange only according to our Return Policy."
                + Environment.NewLine
                + "We have made every effort to display as accurately as possible the colors and images of our products that appear at the store. We cannot guarantee that your computer monitor's display of any color will be accurate."
                + Environment.NewLine
                + "We reserve the right, but are not obligated, to limit the sales of our products or Services to any person, geographic region or jurisdiction. We may exercise this right on a case-by-case basis. We reserve the right to limit the quantities of any products or services that we offer. All descriptions of products or product pricing are subject to change at anytime without notice, at the sole discretion of us. We reserve the right to discontinue any product at any time. Any offer for any product or service made on this app is void where prohibited."
                + Environment.NewLine
                + "We do not warrant that the quality of any products, services, information, or other material purchased or obtained by you will meet your expectations, or that any errors in the Service will be corrected.",
            };
            innerLayout.Children.Add(label12);
        }
    }
}


