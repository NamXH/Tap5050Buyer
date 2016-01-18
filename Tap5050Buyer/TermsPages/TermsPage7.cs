using System;

using Xamarin.Forms;

namespace Tap5050Buyer
{
    public class TermsPage7 : ContentPage
    {
        public TermsPage7()
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

            var label13 = new Label
                {
                    Text = "SECTION 6 - ACCURACY OF BILLING AND ACCOUNT INFORMATION",
                    FontAttributes = FontAttributes.Bold,
                };
            innerLayout.Children.Add(label13);

            var label14 = new Label
                {
                    Text = "We reserve the right to refuse any order you place with us. We may, in our sole discretion, limit or cancel quantities purchased per person, per household or per order. These restrictions may include orders placed by or under the same customer account, the same credit card, and/or orders that use the same billing and/or shipping address. In the event that we make a change to or cancel an order, we may attempt to notify you by contacting the e-mail and/or billing address/phone number provided at the time the order was made. We reserve the right to limit or prohibit orders that, in our sole judgment, appear to be placed by dealers, resellers or distributors."
                        + Environment.NewLine
                        + "You agree to provide current, complete and accurate purchase and account information for all purchases made at our store. You agree to promptly update your account and other information, including your email address and credit card numbers and expiration dates, so that we can complete your transactions and contact you as needed."
                        + Environment.NewLine
                        + "Due to the nature of raffles, all sales are final and there are no refunds on purchased tickets.",
                };
            innerLayout.Children.Add(label14);

        }
    }
}


