using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using XLabs.Forms.Controls;
using Contacts.Plugin;
using System.Linq;
using Contacts.Plugin.Abstractions;
using System.Threading.Tasks;

namespace Tap5050Buyer
{
    // TO DO: some pages need to be refactored to a better MVVM architecture
    public partial class RaffleDetailsPage : CarouselPage
    {
        // Facebook and Twitter account info
        private const string c_facebookAppID = "838514282900661";
        private const string c_twitterAPIKey = "BDXgNpLtpotOT4vFqXLbg8cWE";
        private const string c_twitterSecret = "JGoVsaWvqpJS6HdVPlSVZ2tYloFaXDni7TFWhfthL1LJrsAgh7";
        private const string c_facebookMessageTemplate = "I bought a {0} ticket from {1} to support the good they do in our community. If you would like a chance to win a great prize and support their efforts you can buy a ticket at {2}.";
        // Raffle Name, Organization, ECOMMERCE SITE SHORTENED URL

        public bool LocationDetected { get; set; }

        public bool IncludeSocialMedia { get; set; }

        public Color BackgroundColor { get; set; }

        public RaffleDetailsPage(bool locationDetected, IList<RaffleEvent> raffleEvents, int selectedRaffleId, bool includeSocialMedia)
        {
            InitializeComponent();
            if (!includeSocialMedia)
            {
                Title = "Raffle Details";
            }
            else
            {
                Title = "Choose Media";
                BackgroundColor = Color.FromRgb(240, 248, 255);
            }

            LocationDetected = locationDetected;
            IncludeSocialMedia = includeSocialMedia;

            foreach (var raffle in raffleEvents)
            {
                var page = CreateRaffleEventDetailsPage(raffle);
                Children.Add(page);
                if (raffle.Id == selectedRaffleId)
                {
                    this.SelectedItem = page;
                }
            }

            MessagingCenter.Subscribe<ContactsViewModel>(this, "Done", (sender) =>
                {
                    Navigation.PopAsync();
                });
        }

        // Can change to use BindingContext here later !!
        public ContentPage CreateRaffleEventDetailsPage(RaffleEvent raffle)
        {
            var page = new ContentPage();
            page.BackgroundColor = BackgroundColor;

            var layout = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(20, 5, 20, 5),
            };
            page.Content = layout;

            #region Jackpot
            if (raffle.HasJackpot == "Y")
            {
                var jackpotDescription = new Label
                {
                    Text = raffle.JackpotDescription,
                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                };
                layout.Children.Add(jackpotDescription);

                var jackpotTotal = new Label
                {
                    Text = "$" + raffle.JackpotTotal,
                    TextColor = Color.Red,
                    FontSize = 35,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                };
                layout.Children.Add(jackpotTotal);
            }
            #endregion

            var imageAndNameLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            layout.Children.Add(imageAndNameLayout);

            #region Image and Licence Number
            var imageLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };
            imageAndNameLayout.Children.Add(imageLayout);

            var quarterScreenSize = (DeviceService.Device.Display.Width - layout.Padding.Left - layout.Padding.Right) / 4.5;
            var image = new Image
            {
                Source = ImageSource.FromUri(new Uri(raffle.ImageUrl)),
                WidthRequest = quarterScreenSize,
                HeightRequest = quarterScreenSize,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };
            imageLayout.Children.Add(image);

            if (!String.IsNullOrWhiteSpace(raffle.LicenceNumber))
            {
                var licenceLabel = new Label
                {
                    Text = "Licence # " + raffle.LicenceNumber,
                    FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                };
                imageLayout.Children.Add(licenceLabel);
            }
            #endregion

            #region Name and Organization
            var nameLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };
            imageAndNameLayout.Children.Add(nameLayout);

            var nameLabel = new Label
            {
                Text = raffle.Name,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Center,
            };
            nameLayout.Children.Add(nameLabel);

            var organizationLabel = new Label
            {
                Text = raffle.Organization,
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Center,
            };
            nameLayout.Children.Add(organizationLabel);
            #endregion

            var descriptionLabel = new Label
            {
                Text = raffle.Description,
                HorizontalOptions = LayoutOptions.StartAndExpand,
            };
            layout.Children.Add(descriptionLabel);

            #region See Prizes and Buy Tickets buttons
            if (!IncludeSocialMedia)
            {
                var buttonsLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                };
                layout.Children.Add(buttonsLayout);

                var prizeButton = new Button
                {
                    Text = "See Prizes",
                    BorderColor = Color.Blue,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                };
                prizeButton.Clicked += (sender, e) =>
                {
//                var browser = new WebView();
//                browser.Source = raffle.PrizeUrl;
//
//                var browserPage = new ContentPage();
//                browserPage.Content = browser;
//                browserPage.Title = "See Prizes";
//
//                this.Navigation.PushAsync(browserPage);

                    Device.OpenUri(new Uri(raffle.PrizeUrl)); // External browser
                };
                buttonsLayout.Children.Add(prizeButton);

                var buyButton = new Button
                {
                    Text = "Buy Tickets",
                    BorderColor = Color.Blue,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                };
                buyButton.Clicked += (sender, e) =>
                {
                    var browser = new WebView();
                    browser.Source = raffle.BuyTicketUrl;


//                browser.Navigated += async (object obj, WebNavigatedEventArgs eventArgs) =>
//                {
//                    Debug.WriteLine(eventArgs.Url);
//                    var action = await DisplayActionSheet("ActionSheet: Send to?", "Cancel", null, "Email", "Twitter", "Facebook");
//                    Debug.WriteLine("Action: " + action);
//                };

                    var browserPage = new ContentPage();
                    browserPage.Content = browser;
                    browserPage.Title = "Buy Tickets";

                    this.Navigation.PushAsync(browserPage);
                };
                buttonsLayout.Children.Add(buyButton);
            }
            #endregion

            if (!LocationDetected && !IncludeSocialMedia)
            {
                var locationWarning = new Label
                {
                    Text = "You may not complete the buy since we cannot detect your location.",
                    FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                    TextColor = Color.Red,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.End,
                };
                layout.Children.Add(locationWarning);
            }

            if (IncludeSocialMedia)
            {
                var socialShare = DependencyService.Get<ISocialShare>();

                var socialMediaLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Padding = new Thickness(5, 5, 5, 5),
                    VerticalOptions = LayoutOptions.End,
                };
                layout.Children.Add(socialMediaLayout);

                var facebookButton = new ImageButton
                {
                    ImageHeightRequest = 50,
                    ImageWidthRequest = 50,
                    Source = "facebook.png",
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                };
                socialMediaLayout.Children.Add(facebookButton);
                facebookButton.Clicked += (sender, e) =>
                {
                    socialShare.Facebook(c_facebookAppID, String.Format(c_facebookMessageTemplate, raffle.Name, raffle.Organization, raffle.BuyTicketUrl), raffle.BuyTicketUrl);
                };

                var twitterButton = new ImageButton
                {
                    ImageHeightRequest = 50,
                    ImageWidthRequest = 50,
                    Source = "twitter.png",
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                };
                socialMediaLayout.Children.Add(twitterButton);
                twitterButton.Clicked += (sender, e) =>
                {
                    socialShare.Twitter(c_twitterAPIKey, c_twitterSecret, String.Format(c_facebookMessageTemplate, raffle.Name, raffle.Organization, raffle.BuyTicketUrl), raffle.BuyTicketUrl);
                };

                var smsButton = new ImageButton
                {
                    ImageHeightRequest = 50,
                    ImageWidthRequest = 50,
                    Source = "sms.png",
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                };
                socialMediaLayout.Children.Add(smsButton);
                smsButton.Clicked += async (sender, e) =>
                {
                    var allContacts = await GetAllContacts();
                    if (allContacts != null)
                    {
                        var contactsWithAMobileNumber = allContacts.Where(x => x.Phones.Any(y => y.Label.Equals("Mobile", StringComparison.OrdinalIgnoreCase)));

                        var extendedContacts = new List<ExtendedContact>();
                        foreach (var contact in contactsWithAMobileNumber)
                        {
                            extendedContacts.Add(new ExtendedContact(contact));
                        }

                        Navigation.PushAsync(new ContactsPage(extendedContacts, 0));
                    }
                };

                var emailButton = new ImageButton
                {
                    ImageHeightRequest = 50,
                    ImageWidthRequest = 50,
                    Source = "email.png",
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                };
                socialMediaLayout.Children.Add(emailButton);
                emailButton.Clicked += async (sender, e) =>
                {
                    var allContacts = await GetAllContacts();
                    if (allContacts != null)
                    {
                        var contactsWithAnEmail = allContacts.Where(x => x.Emails.Any());

                        var extendedContacts = new List<ExtendedContact>();
                        foreach (var contact in contactsWithAnEmail)
                        {
                            extendedContacts.Add(new ExtendedContact(contact));
                        }

                        Navigation.PushAsync(new ContactsPage(extendedContacts, 1));
                    }
                };
            }

            return page;
        }

        public async Task<IQueryable<Contact>> GetAllContacts()
        {
            if (await CrossContacts.Current.RequestPermission())
            {
                CrossContacts.Current.PreferContactAggregation = false; //recommended by author 

                return CrossContacts.Current.Contacts;
            }
            else
            {
                return null;
            }
        }
    }
}