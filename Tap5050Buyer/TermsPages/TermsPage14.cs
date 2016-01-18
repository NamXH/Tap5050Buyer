using System;

using Xamarin.Forms;

namespace Tap5050Buyer
{
    public class TermsPage14 : ContentPage
    {
        public TermsPage14()
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

            var label27 = new Label
            {
                Text = "SECTION 13 - DISCLAIMER OF WARRANTIES; LIMITATION OF LIABILITY",
                FontAttributes = FontAttributes.Bold,
            };
            innerLayout.Children.Add(label27);

            var label28 = new Label
            {
                Text = "We do not guarantee, represent or warrant that your use of our service will be uninterrupted, timely, secure or error-free."
                + Environment.NewLine
                + "We do not warrant that the results that may be obtained from the use of the service will be accurate or reliable."
                + Environment.NewLine
                + "You agree that from time to time we may remove the service for indefinite periods of time or cancel the service at any time, without notice to you."
                + Environment.NewLine
                + "You expressly agree that your use of, or inability to use, the service is at your sole risk. The service and all products and services delivered to you through the service are (except as expressly stated by us) provided 'as is' and 'as available' for your use, without any representation, warranties or conditions of any kind, either express or implied, including all implied warranties or conditions of merchantability, merchantable quality, fitness for a particular purpose, durability, title, and non-infringement."
                + Environment.NewLine
                + "In no case shall TAP 5050, our directors, officers, employees, affiliates, agents, contractors, interns, suppliers, service providers or licensors be liable for any injury, loss, claim, or any direct, indirect, incidental, punitive, special, or consequential damages of any kind, including, without limitation lost profits, lost revenue, lost savings, loss of data, replacement costs, or any similar damages, whether based in contract, tort (including negligence), strict liability or otherwise, arising from your use of any of the service or any products procured using the service, or for any other claim related in any way to your use of the service or any product, including, but not limited to, any errors or omissions in any content, or any loss or damage of any kind incurred as a result of the use of the service or any content (or product) posted, transmitted, or otherwise made available via the service, even if advised of their possibility. Because some states or jurisdictions do not allow the exclusion or the limitation of liability for consequential or incidental damages, in such states or jurisdictions, our liability shall be limited to the maximum extent permitted by law.",
            };
            innerLayout.Children.Add(label28);

        }
    }
}


