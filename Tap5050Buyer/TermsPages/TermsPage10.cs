using System;

using Xamarin.Forms;

namespace Tap5050Buyer
{
    public class TermsPage10 : ContentPage
    {
        public TermsPage10()
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

            var label19 = new Label
                {
                    Text = "SECTION 9 - USER COMMENTS, FEEDBACK AND OTHER SUBMISSIONS",
                    FontAttributes = FontAttributes.Bold,
                };
            innerLayout.Children.Add(label19);

            var label20 = new Label
                {
                    Text = "If, at our request, you send certain specific submissions (for example contest entries) or without a request from us you send creative ideas, suggestions, proposals, plans, or other materials, whether online, by email, by postal mail, or otherwise (collectively, 'comments'), you agree that we may, at any time, without restriction, edit, copy, publish, distribute, translate and otherwise use in any medium any comments that you forward to us. We are and shall be under no obligation (1) to maintain any comments in confidence; (2) to pay compensation for any comments; or (3) to respond to any comments."
                        + Environment.NewLine
                        + "We may, but have no obligation to, monitor, edit or remove content that we determine in our sole discretion are unlawful, offensive, threatening, libelous, defamatory, pornographic, obscene or otherwise objectionable or violates any party's intellectual property or these Terms of Service."
                        + Environment.NewLine
                        + "You agree that your comments will not violate any right of any third-party, including copyright, trademark, privacy, personality or other personal or proprietary right. You further agree that your comments will not contain libelous or otherwise unlawful, abusive or obscene material, or contain any computer virus or other malware that could in any way affect the operation of the Service or any related application. You may not use a false e-mail address, pretend to be someone other than yourself, or otherwise mislead us or third-parties as to the origin of any comments. You are solely responsible for any comments you make and their accuracy. We take no responsibility and assume no liability for any comments posted by you or any third-party.",
                };
            innerLayout.Children.Add(label20);
        }
    }
}


