using System;

using Xamarin.Forms;

namespace Tap5050Buyer
{
    public class TermsPage2 : ContentPage
    {
        public TermsPage2()
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

            var label03 = new Label
            {
                Text = "SECTION 1 - ONLINE STORE TERMS",
                FontAttributes = FontAttributes.Bold,
            };
            innerLayout.Children.Add(label03);

            var label04 = new Label
            {
                Text = "By agreeing to these Terms of Service, you represent that you are at least the age of majority in your state or province of residence and of a legal age to purchase or sell a raffle ticket in said state or province."
                + Environment.NewLine
                + "You may not use our products for any illegal or unauthorized purpose nor may you, in the use of the Service, violate any laws in your jurisdiction (including but not limited to copyright laws)."
                + Environment.NewLine
                + "You must not transmit any worms or viruses or any code of a destructive nature."
                + Environment.NewLine
                + "A breach or violation of any of the Terms will result in an immediate termination of your Services.",
            };
            innerLayout.Children.Add(label04);
        }
    }
}


