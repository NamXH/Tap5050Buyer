using System;

using Xamarin.Forms;

namespace Tap5050Buyer
{
    public class TermsPage18 : ContentPage
    {
        public TermsPage18()
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

            var label35 = new Label
            {
                Text = "SECTION 17 - ENTIRE AGREEMENT",
                FontAttributes = FontAttributes.Bold,
            };
            innerLayout.Children.Add(label35);

            var label36 = new Label
            {
                Text = "The failure of us to exercise or enforce any right or provision of these Terms of Service shall not constitute a waiver of such right or provision."
                + Environment.NewLine
                + "These Terms of Service and any policies or operating rules posted by us on this app or in respect to The Service constitutes the entire agreement and understanding between you and us and govern your use of the Service, superseding any prior or contemporaneous agreements, communications and proposals, whether oral or written, between you and us (including, but not limited to, any prior versions of the Terms of Service)."
                + Environment.NewLine
                + "Any ambiguities in the interpretation of these Terms of Service shall not be construed against the drafting party.",
            };
            innerLayout.Children.Add(label36);
        }
    }
}


