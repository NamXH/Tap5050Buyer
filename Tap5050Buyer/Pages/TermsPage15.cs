using System;

using Xamarin.Forms;

namespace Tap5050Buyer
{
    public class TermsPage15 : ContentPage
    {
        public TermsPage15()
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

            var label29 = new Label
            {
                Text = "SECTION 14 - INDEMNIFICATION",
                FontAttributes = FontAttributes.Bold,
            };
            innerLayout.Children.Add(label29);

            var label30 = new Label
            {
                Text = "You agree to indemnify, defend and hold harmless TAP 5050 and our parent, subsidiaries, affiliates, partners, officers, directors, agents, contractors, licensors, service providers, subcontractors, suppliers, interns and employees, harmless from any claim or demand, including reasonable attorneys' fees, made by any third-party due to or arising out of your breach of these Terms of Service or the documents they incorporate by reference, or your violation of any law or the rights of a third-party."
            };
            innerLayout.Children.Add(label30); 
        }
    }
}


