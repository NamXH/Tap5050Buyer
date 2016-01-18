using System;

using Xamarin.Forms;

namespace Tap5050Buyer
{
    public class TermsPage21 : ContentPage
    {
        public TermsPage21()
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

            var label41 = new Label
            {
                Text = "SECTION 20 - CONTACT INFORMATION",
                FontAttributes = FontAttributes.Bold,
            };
            innerLayout.Children.Add(label41);

            var label42 = new Label
            {
                Text = "Questions about the Terms of Service should be sent to us at info@tap5050.com."
            };
            innerLayout.Children.Add(label42);
        }
    }
}


