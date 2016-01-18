using System;

using Xamarin.Forms;

namespace Tap5050Buyer
{
    public class TermsPage4 : ContentPage
    {
        public TermsPage4()
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

            var label07 = new Label
            {
                Text = "SECTION 3 - ACCURACY, COMPLETENESS AND TIMELINESS OF INFORMATION",
                FontAttributes = FontAttributes.Bold,
            };
            innerLayout.Children.Add(label07);

            var label08 = new Label
            {
                Text = "We are not responsible if information made available in this app is not accurate, complete or current. The material on this app is provided for general information only and should not be relied upon or used as the sole basis for making decisions without consulting primary, more accurate, more complete or more timely sources of information. Any reliance on the material in this app is at your own risk."
                + Environment.NewLine
                + "This app may contain certain historical information. Historical information, necessarily, is not current and is provided for your reference only. We reserve the right to modify the contents of this app at any time, but we have no obligation to update any information on our app. You agree that it is your responsibility to monitor changes to our app.",
            };
            innerLayout.Children.Add(label08);
        }
    }
}


