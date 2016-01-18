using System;

using Xamarin.Forms;

namespace Tap5050Buyer
{
    public class TermsPage16 : ContentPage
    {
        public TermsPage16()
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

            var label31 = new Label
            {
                Text = "SECTION 15 - SEVERABILITY",
                FontAttributes = FontAttributes.Bold,
            };
            innerLayout.Children.Add(label31);

            var label32 = new Label
            {
                Text = "In the event that any provision of these Terms of Service is determined to be unlawful, void or unenforceable, such provision shall nonetheless be enforceable to the fullest extent permitted by applicable law, and the unenforceable portion shall be deemed to be severed from these Terms of Service, such determination shall not affect the validity and enforceability of any other remaining provisions."
            };
            innerLayout.Children.Add(label32);
        }
    }
}


