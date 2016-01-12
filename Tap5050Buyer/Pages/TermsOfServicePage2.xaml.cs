using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Tap5050Buyer
{
    public partial class TermsOfServicePage2 : ContentPage
    {
        public TermsOfServicePage2()
        {
            InitializeComponent();
            Title = "Terms of Service";
            NavigationPage.SetBackButtonTitle(this, "Back");

            var layout = new StackLayout();
            this.Content = layout;

            var listView = new ListView();
            layout.Children.Add(listView);

            listView.ItemsSource = new string[]
            {
                "1. OVERVIEW",
                "2. ONLINE STORE TERMS",
                "3. GENERAL CONDITIONS",
                "4. CHANGES TO TERMS OF SERVICE",
                "5. ACCURACY, COMPLETENESS AND TIMELINESS OF INFORMATION",
                "6. PRODUCTS OR SERVICES (if applicable)",
                "7. ACCURACY OF BILLING AND ACCOUNT INFORMATION",
                "8. OPTIONAL TOOLS",
                "9. THIRD-PARTY LINKS",
                "10. USER COMMENTS, FEEDBACK AND OTHER SUBMISSIONS",
                "11. PERSONAL INFORMATION",
                "12. ERRORS, INACCURACIES AND OMISSIONS",
                "13. PROHIBITED USES",
                "14. DISCLAIMER OF WARRANTIES; LIMITATION OF LIABILITY",
                "15. INDEMNIFICATION",
                "16. SEVERABILITY",
                "17. TERMINATION",
                "18. ENTIRE AGREEMENT",
                "19. GOVERNING LAW",
                "20. CHANGES TO TERMS OF SERVICE",
                "21. CONTACT INFORMATION",
            };

            listView.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem != null)
                {
                    var start = "";
                        
                    var str = (string)e.SelectedItem;
                    if (!String.IsNullOrEmpty(str))
                    {
                        start = str.Substring(0, 2);
                    }

                    switch (start)
                    {
                        case "1.":
                            this.Navigation.PushAsync(new TermsPage1());
                            break;
                        case "2.":
                            this.Navigation.PushAsync(new TermsPage2());
                            break;
                        case "3.":
                            this.Navigation.PushAsync(new TermsPage3());
                            break;
                        case "4.":
                            this.Navigation.PushAsync(new TermsPage4());
                            break;
                        case "5.":
                            this.Navigation.PushAsync(new TermsPage5());
                            break;
                        case "6.":
                            this.Navigation.PushAsync(new TermsPage6());
                            break;
                        case "7.":
                            this.Navigation.PushAsync(new TermsPage7());
                            break;
                        case "8.":
                            this.Navigation.PushAsync(new TermsPage8());
                            break;
                        case "9.":
                            this.Navigation.PushAsync(new TermsPage9());
                            break;
                        case "10":
                            this.Navigation.PushAsync(new TermsPage10());
                            break;
                        case "11":
                            this.Navigation.PushAsync(new TermsPage11());
                            break;
                        case "12":
                            this.Navigation.PushAsync(new TermsPage12());
                            break;
                        case "13":
                            this.Navigation.PushAsync(new TermsPage13());
                            break;
                        case "14":
                            this.Navigation.PushAsync(new TermsPage14());
                            break;
                        case "15":
                            this.Navigation.PushAsync(new TermsPage15());
                            break;
                        case "16":
                            this.Navigation.PushAsync(new TermsPage16());
                            break;
                        case "17":
                            this.Navigation.PushAsync(new TermsPage17());
                            break;
                        case "18":
                            this.Navigation.PushAsync(new TermsPage18());
                            break;
                        case "19":
                            this.Navigation.PushAsync(new TermsPage19());
                            break;
                        case "20":
                            this.Navigation.PushAsync(new TermsPage20());
                            break;
                        case "21":
                            this.Navigation.PushAsync(new TermsPage21());
                            break;
                        default:
                            break;
                    }

                    listView.SelectedItem = null;
                }
            };
        }
    }
}