using System;

using Xamarin.Forms;

namespace Tap5050Buyer
{
    public class TermsPage9 : ContentPage
    {
        public TermsPage9()
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

            var label17 = new Label
            {
                Text = "SECTION 8 - THIRD-PARTY LINKS",
                FontAttributes = FontAttributes.Bold,
            };
            innerLayout.Children.Add(label17);

            var label18 = new Label
            {
                Text = "Certain content, products and services available via our Service may include materials from third-parties."
                + Environment.NewLine
                + "Third-party links on this app may direct you to third-party websites that are not affiliated with us. We are not responsible for examining or evaluating the content or accuracy and we do not warrant and will not have any liability or responsibility for any third-party materials or websites, or for any other materials, products, or services of third-parties."
                + Environment.NewLine
                + "We are not liable for any harm or damages related to the purchase or use of goods, services, resources, content, or any other transactions made in connection with any third-party websites. Please review carefully the third-party's policies and practices and make sure you understand them before you engage in any transaction. Complaints, claims, concerns, or questions regarding third-party products should be directed to the third-party.",
            };
            innerLayout.Children.Add(label18);
        }
    }
}


