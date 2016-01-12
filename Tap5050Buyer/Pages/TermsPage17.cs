using System;

using Xamarin.Forms;

namespace Tap5050Buyer
{
    public class TermsPage17 : ContentPage
    {
        public TermsPage17()
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

            var label33 = new Label
            {
                Text = "SECTION 16 - TERMINATION",
                FontAttributes = FontAttributes.Bold,
            };
            innerLayout.Children.Add(label33);

            var label34 = new Label
            {
                Text = "The obligations and liabilities of the parties incurred prior to the termination date shall survive the termination of this agreement for all purposes."
                + Environment.NewLine
                + "These Terms of Service are effective unless and until terminated by either you or us. You may terminate these Terms of Service at any time by notifying us that you no longer wish to use our Services, or when you cease using our app."
                + Environment.NewLine
                + "If in our sole judgment you fail, or we suspect that you have failed, to comply with any term or provision of these Terms of Service, we also may terminate this agreement at any time without notice and you will remain liable for all amounts due up to and including the date of termination; and/or accordingly may deny you access to our Services (or any part thereof).",
            };
            innerLayout.Children.Add(label34);
        }
    }
}


