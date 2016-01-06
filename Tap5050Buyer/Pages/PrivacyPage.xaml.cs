using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Tap5050Buyer
{
    public partial class PrivacyPage : ContentPage
    {
        public PrivacyPage()
        {
            InitializeComponent();
            Title = "Privacy Policy";
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

            var label01 = new Label
            {
                Text = "This privacy policy explains how Tap5050 handles your personal information and data. We value your trust, so we've strived to present this policy in clear, plain language instead of legalese.",
                FontAttributes = FontAttributes.Bold,
            };
            innerLayout.Children.Add(label01);

            var label02 = new Label
            {
                Text = "Tap5050 is the owner of all customer data that has been entered into our system whether that data has come from online or electronic (raffle terminal) sales. As such we take the privacy of our customers very seriously.",
            };
            innerLayout.Children.Add(label02);

            var label03 = new Label
            {
                Text = "With whom do we share your information?",
                FontAttributes = FontAttributes.Bold,
            };
            innerLayout.Children.Add(label03);

            var label04 = new Label
            {
                Text = "We recognize that you have entrusted us with safeguarding the privacy of your information. Because that trust is very important to us, the only time we will disclose or share your personal information with a third party would be for one of the five following reasons, (a) if required by law; (b) purposes of providing you services such as using a payment processor; (c) de-identified or aggregated the information so that individuals or other entities cannot reasonably be identified by it; (d) In case of a corporate restructuring or acquisition context; (e) The presence of a cookie to advertise our services. We may ask advertising networks and exchanges to display ads promoting our services on other websites. We may ask them to deliver those ads based on the presence of a cookie, but in doing so will not share any other personal information with the advertiser. Our advertising network partners may use cookies and page tags or web beacons to collect certain non-personal information about your activities on this and other websites to provide you with targeted advertising.",
            };
            innerLayout.Children.Add(label04);

            var label05 = new Label
            {
                Text = "Main Privacy Points:",
                FontAttributes = FontAttributes.Bold,
            };
            innerLayout.Children.Add(label05);

            var label06 = new Label
            {
                Text = "- Tap5050 will not rent, trade, sell or share your personal information with third party advertisers or marketers. Personal information collected will be used to fulfill ticket orders, provide information on future raffles, provide jackpot and winning number information, contact winners and publicize the names of prize winners."
                + Environment.NewLine
                + "- We do allow marketing rights to charities if a customer indicates that he/she would like to receive marketing information from a charity. Although the charity can send you a message if you allow it, they do not have access to your email, telephone, credit card, name or any personal information. Tap5050 is the custodian of your personal information and we follow best practices to keep this information safe."
                + Environment.NewLine
                + "- Tap5050 will market to you directly on behalf of the charity. In this manner we do not disclose your personal information to the charity."
                + Environment.NewLine
                + "- We limit the amount of information a charity can send you. Even if you decide you want to receive information from the charity, we limit the amount of messages a charity may send you."
                + Environment.NewLine
                + "- Tap5050 does not retain your credit card information. Your credit card information is protected by stripe.com and a tokenized system is used for transactional purposes. Stripe.com is the only third party service provider we share information with and they are contractually bound to keep your information confidential. By using our services, you authorize Tap5050 to sub-contract in this manner on your behalf.",
            };
            innerLayout.Children.Add(label06);

            var label07 = new Label
            {
                Text = "Passive or indirect information",
                FontAttributes = FontAttributes.Bold,
            };
            innerLayout.Children.Add(label07);

            var label08 = new Label
            {
                Text = "- Device data: We may collect data from the device and application you use to access services, such as your IP address and browser type. We may also infer your geographic location based on your IP address. Geolocation is used to ensure you are physically located in the jurisdiction of the raffle."
                + Environment.NewLine
                + "- Usage data: We may collect usage data about you whenever you interact with our This may include which webpages you visit, what you click on, when you performed those actions, and so on. Additionally, like most websites today, our web servers keep log files that record data each time a device accesses those servers. The log files contain data about the nature of each access, including originating IP addresses."
                + Environment.NewLine
                + "- Referral data: If you arrive at a Tap5050 website from an external source (such as a link another website) we record information about the source that referred you to us."
                + Environment.NewLine
                + "- Information from page tags: We use third party tracking services that employ cookies page tags (also known as web beacons) to collect aggregated and anonymized data about visitors to our websites. This data includes usage and user statistics.",
            };
            innerLayout.Children.Add(label08);

            var label09 = new Label
            {
                Text = "Other Important Information",
                FontAttributes = FontAttributes.Bold,
            };
            innerLayout.Children.Add(label09);

            var label10 = new Label
            {
                Text = "- Data locations: Our servers are based in Canada, so your personal information will hosted and processed by us in the Candada. Your personal information may also be processed in, or transferred or disclosed to, countries in which Tap5050 subsidiaries and offices are located and in which our service providers are located or have servers."
                + Environment.NewLine
                + "- Security: Details about Tap5050's security practices are available in our Security Statement. We are committed to handling your personal information and data with integrity and care. However, regardless of the security protections and precautions we undertake, there is always a risk that your personal data may be viewed and used by unauthorized third parties as a result of collecting and transmitting your data through the internet.",
            };
            innerLayout.Children.Add(label10);
        }
    }
}

