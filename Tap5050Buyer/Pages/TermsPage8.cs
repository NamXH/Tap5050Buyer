using System;

using Xamarin.Forms;

namespace Tap5050Buyer
{
    public class TermsPage8 : ContentPage
    {
        public TermsPage8()
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

            var label15 = new Label
            {
                Text = "SECTION 7 - OPTIONAL TOOLS",
                FontAttributes = FontAttributes.Bold,
            };
            innerLayout.Children.Add(label15);

            var label16 = new Label
            {
                Text = "We may provide you with access to third-party tools over which we neither monitor nor have any control nor input."
                + Environment.NewLine
                + "You acknowledge and agree that we provide access to such tools \"as is\" and \"as available\" without any warranties, representations or conditions of any kind and without any endorsement. We shall have no liability whatsoever arising from or relating to your use of optional third-party tools."
                + Environment.NewLine
                + "Any use by you of optional tools offered through the app is entirely at your own risk and discretion and you should ensure that you are familiar with and approve of the terms on which tools are provided by the relevant third-party provider(s)."
                + Environment.NewLine
                + "We may also, in the future, offer new services and/or features through the app (including, the release of new tools and resources). Such new features and/or services shall also be subject to these Terms of Service.",
            };
            innerLayout.Children.Add(label16);
        }
    }
}


