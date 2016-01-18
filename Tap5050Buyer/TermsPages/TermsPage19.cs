using System;

using Xamarin.Forms;

namespace Tap5050Buyer
{
    public class TermsPage19 : ContentPage
    {
        public TermsPage19()
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

            var label37 = new Label
            {
                Text = "SECTION 18 - GOVERNING LAW",
                FontAttributes = FontAttributes.Bold,
            };
            innerLayout.Children.Add(label37);

            var label38 = new Label
            {
                Text = "These Terms of Service and any separate agreements whereby we provide you Services shall be governed by and construed in accordance with the laws of Saskatchewan, Canada."
            };
            innerLayout.Children.Add(label38);
        }
    }
}


