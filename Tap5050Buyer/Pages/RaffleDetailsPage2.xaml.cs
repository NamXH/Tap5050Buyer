﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using XLabs.Forms.Controls;
using Contacts.Plugin;
using System.Linq;
using Contacts.Plugin.Abstractions;
using System.Threading.Tasks;
using System.Net.Http;

namespace Tap5050Buyer
{
    // TO DO: some pages may need to be refactored to a better MVVM architecture
    public partial class RaffleDetailsPage2 : ContentPage // Currently only use this for Android
    {
        // Facebook and Twitter account info
        private const string c_facebookAppID = "838514282900661";
        private const string c_twitterAPIKey = "BDXgNpLtpotOT4vFqXLbg8cWE";
        private const string c_twitterSecret = "JGoVsaWvqpJS6HdVPlSVZ2tYloFaXDni7TFWhfthL1LJrsAgh7";
        private const string c_facebookMessageTemplate = "A big shout out to my fb friends: I am supporting {0} and I hope you will too by purchasing a raffle ticket at {1} if you are in {2}";
        private const string c_twitterMessageTemplate = "I am supporting {0} and I hope you will too by purchasing a raffle ticket at";

        public bool LocationDetected { get; set; }

        public Color BackgroundColor { get; set; }

        public RaffleDetailsPage2(bool locationDetected, RaffleEvent selectedRaffle)
        {
            InitializeComponent();
            Title = "Raffle Details";
            NavigationPage.SetBackButtonTitle(this, "Back"); // Set back button title for the next page

            LocationDetected = locationDetected;

            CreateRaffleEventDetailsPage(selectedRaffle);
        }

        // Can change to use BindingContext here later !!
        public void CreateRaffleEventDetailsPage(RaffleEvent raffle)
        {
            this.BackgroundColor = BackgroundColor;

            var layout = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(20, 5, 20, 5),
            };
            this.Content = layout;

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
                WidthRequest = quarterScreenSize,
                HeightRequest = quarterScreenSize,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };
            if (raffle.ImageUrl != "tap5050.png") // Default string set in RaffleListPage if the ImageUrl is null or empty
            {
                image.Source = ImageSource.FromUri(new Uri(raffle.ImageUrl));
            }
            else
            {
                image.Source = ImageSource.FromFile("tap5050.png");
            }

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
            var buttonsLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
            };
            layout.Children.Add(buttonsLayout);

            var prizeButton = new Button
            {
                Text = "See Prizes",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BorderColor = Color.Accent,
                BorderWidth = 1,
                BorderRadius = 12,
                HeightRequest = 46,
                WidthRequest = 120,
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

                try
                {
                    Device.OpenUri(new Uri(raffle.PrizeUrl)); // External browser
                }
                catch (Exception ex)
                {
                    DisplayAlert("Error", ex.Message + Environment.NewLine + "Please try again later.", "OK");
                }
            };
            buttonsLayout.Children.Add(prizeButton);

            var buyButton = new Button
            {
                Text = "Buy Tickets",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BorderColor = Color.Accent,
                BorderWidth = 1,
                BorderRadius = 12,
                HeightRequest = 46,
                WidthRequest = 120,
            };
            buyButton.Clicked += async (sender, e) =>
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        var response = await client.GetAsync(raffle.BuyTicketUrl); // Workaround: WebView doesn't have a method to notify us when the Source is invalid or there is no Internet connection
                    }

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
                }
                catch (Exception ex)
                {
                    DisplayAlert("Error", ex.Message + Environment.NewLine + "Please try again later.", "OK");
                }
            };
            buttonsLayout.Children.Add(buyButton);
            #endregion

            if (!LocationDetected)
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

            #region Social Share
            var socialShare = DependencyService.Get<ISocialShare>();

            var socialMediaLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(5, 5, 5, 5),
                VerticalOptions = LayoutOptions.End,
            };
            layout.Children.Add(socialMediaLayout);

            var buttonSize = Device.OnPlatform<int>(50, 80, 50);

            var facebookButton = new ImageButton
            {
                ImageHeightRequest = buttonSize,
                ImageWidthRequest = buttonSize,
                BackgroundColor = Color.Transparent,
                Source = "facebook.png",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            socialMediaLayout.Children.Add(facebookButton);
            facebookButton.Clicked += async (sender, e) =>
            {
//                var facebookIsReachable = true;
//                try
//                {
//                    using (var client = new HttpClient())
//                    {
//                        var response = await client.GetAsync("https://m.facebook.com"); // Workaround the infinite FB authentication pop-up when there is no Internet
//                        if ((int)response.StatusCode >= 400)
//                        {
//                            throw new Exception("Connection error with status code " + response.StatusCode);
//                        }
//                    }
//                }
//                catch (Exception ex)
//                {
//                    facebookIsReachable = false;
//                    DisplayAlert("Error", "Facebook is unreachable. Please check your connection and try again." + Environment.NewLine + ex.Message, "OK");
//                }
//
//                if (facebookIsReachable)
//                {
//                    socialShare.Facebook(c_facebookAppID, String.Format(c_facebookMessageTemplate, raffle.Organization, raffle.BuyTicketUrl, raffle.LocationName), raffle.BuyTicketUrl);
//                }
                socialShare.Facebook(c_facebookAppID, String.Format(c_facebookMessageTemplate, raffle.Organization, raffle.BuyTicketUrl, raffle.LocationName), raffle.BuyTicketUrl, raffle.ImageUrl, raffle.Name, raffle.Organization);
            };

            var twitterButton = new ImageButton
            {
                ImageHeightRequest = buttonSize,
                ImageWidthRequest = buttonSize,
                BackgroundColor = Color.Transparent,
                Source = "twitter.png",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            socialMediaLayout.Children.Add(twitterButton);
            twitterButton.Clicked += (sender, e) =>
            {
                var orgName = raffle.Organization.Substring(0, Math.Min(raffle.Organization.Length, 40)); // Truncate the name if exceed 40 chars
                socialShare.Twitter(c_twitterAPIKey, c_twitterSecret, String.Format(c_twitterMessageTemplate, orgName), raffle.BuyTicketUrl);
            };

            var smsButton = new ImageButton
            {
                ImageHeightRequest = buttonSize,
                ImageWidthRequest = buttonSize,
                BackgroundColor = Color.Transparent,
                Source = "sms.png",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            socialMediaLayout.Children.Add(smsButton);
            smsButton.Clicked += async (sender, e) =>
            {
                var allContacts = await GetAllContacts();
                if (allContacts != null)
                {
                    var all = allContacts.ToList();
                    var contactsWithAMobileNumber = all.Where(x => x.Phones.Any(y => y.Label.Equals("Mobile", StringComparison.OrdinalIgnoreCase)));

                    var extendedContacts = new List<ExtendedContact>();
                    foreach (var contact in contactsWithAMobileNumber)
                    {
                        extendedContacts.Add(new ExtendedContact(contact));
                    }

                    Navigation.PushAsync(new ContactsPage(extendedContacts, 0, raffle));
                }
            };

            var emailButton = new ImageButton
            {
                ImageHeightRequest = buttonSize,
                ImageWidthRequest = buttonSize,
                BackgroundColor = Color.Transparent,
                Source = "email.png",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            socialMediaLayout.Children.Add(emailButton);
            emailButton.Clicked += async (sender, e) =>
            {
                var allContacts = await GetAllContacts();
                if (allContacts != null)
                {
                    // var contactsWithAnEmail = allContacts.Where(x => x.Emails.Any()); // This causes a bug in IQueryable, we .ToList() to work around!!
                    var all = allContacts.ToList();
                    var contactsWithAnEmail = all.Where(x => x.Emails.Any());

                    var extendedContacts = new List<ExtendedContact>();
                    foreach (var contact in contactsWithAnEmail)
                    {
                        extendedContacts.Add(new ExtendedContact(contact));
                    }

                    Navigation.PushAsync(new ContactsPage(extendedContacts, 1, raffle));
                }
            };
            #endregion
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