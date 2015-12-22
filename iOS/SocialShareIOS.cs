using System;
using Xamarin.Social.Services;
using Xamarin.Social;
using Xamarin.Auth;
using System.Collections.Generic;
using UIKit;
using MessageUI;
using Foundation;
using Tap5050Buyer.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(SocialShareIOS))]
namespace Tap5050Buyer.iOS
{
    public class SocialShareIOS: UIViewController, ISocialShare
    {
        public SocialShareIOS()
        {
        }

        public void printConsoleString(string message)
        {
            Console.WriteLine(message);
        }

        public void ResetLogin()
        {
            //delete all facebook accounts
            IEnumerable<Account> facebookAccounts = AccountStore.Create().FindAccountsForService("Facebook");
            foreach (var x in facebookAccounts)
            {
                AccountStore.Create().Delete(x, "Facebook");
            }

            //delete all twitter accounts
            IEnumerable<Account> twitterAccounts = AccountStore.Create().FindAccountsForService("Twitter");
            foreach (var x in twitterAccounts)
            {
                AccountStore.Create().Delete(x, "Twitter");
            }
        }

        public void Email(string message, string subject, string[] recivers)
        {
            MFMailComposeViewController mailController = new MFMailComposeViewController();
            if (!MFMailComposeViewController.CanSendMail)
            {
                return;
            }
            if (recivers != null)
            {
                mailController.SetToRecipients(recivers);
            }
            mailController.SetSubject(subject);
            mailController.SetMessageBody(message, false);
            mailController.Finished += ( s, args) =>
            {
                Console.WriteLine(args.Result.ToString());
                args.Controller.DismissViewController(true, null);
            };

            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(mailController, true, null);
        }

        public void Sms(string message, string[] recivers)
        {

            if (!MFMessageComposeViewController.CanSendText)
            {
                return;
            }	

            var smsController = new MFMessageComposeViewController();
            if (recivers != null)
            {
                smsController.Recipients = recivers;
            }	
            smsController.Body = message;
            smsController.Finished += (sender, e) => smsController.DismissViewController(true, null);
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(smsController, true, null);
        }

        public void Facebook(string clientID, string message, string link)
        {
            //check if there is a stored account
            Account facebookAccount = new Account(); 
            bool hasStoredAccout = false;
		
            IEnumerable<Account> facebookAccounts = AccountStore.Create().FindAccountsForService("Facebook");
            foreach (var x in facebookAccounts)
            {
                facebookAccount = x;
                hasStoredAccout = true;
            }	

            if (!hasStoredAccout)
            {
                FacebookLoginPost(clientID, message, link);
            }
            else
            {
                FacebookPost(facebookAccount, message, clientID, link);
            }
        }

        private void FacebookLoginPost(string clientID, string message, string link)
        {
            var auth = new OAuth2Authenticator(
                           clientId: clientID,
                           scope: "publish_actions",
                           authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
                           redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html")
                       );

            auth.Completed += (sender, eventArgs) =>
            {
                UIApplication.SharedApplication.KeyWindow.RootViewController.DismissViewController(true, () =>
                    {
                        if (eventArgs.IsAuthenticated)
                        {
                            //save the account
                            AccountStore.Create().Save(eventArgs.Account, "Facebook");
                            //post the message
                            FacebookPost(eventArgs.Account, message, clientID, link);
                        }
                        else
                        {
                            // The user cancelled during authentication.  do nothing

                        }
                    });
            };
            var view = auth.GetUI();
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(view, true, null);
        }


        private void FacebookPost(Account facebookAccount, string message, string clientID, string link)
        {
            var request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me"), null, facebookAccount);
            request.GetResponseAsync().ContinueWith(t =>
                {
                    if (t.IsFaulted)
                    {
                        Console.WriteLine("Error: " + t.Exception.InnerException.Message);
                        FacebookLoginPost(clientID, message, link);
                    }
                    else
                    {
                        // 1. Create the service
                        var facebook = new FacebookService
                        {
                            ClientId = clientID,
                        };

                        facebook.SaveAccount(facebookAccount);

                        // 2. Create an item to share
                        var item = new Item();
                        item.Text = message;
                        if (link != null)
                        {
                            item.Links.Add(new Uri(link));
                        }

                        // 3. Present the UI on iOS
                        InvokeOnMainThread(() =>
                            {
                                var shareController = facebook.GetShareUI(item, result =>
                                    {
                                        UIApplication.SharedApplication.KeyWindow.RootViewController.DismissViewController(true, null);
                                    });
                                UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(shareController, true, null);
                            });
                    }
                });
        }

        public void Twitter(string clientID, string secrete, string message, string link)
        {
            //check if there is a stored account
            Account twitterAccount = new Account(); 
            bool hasStoredAccout = false;

            IEnumerable<Account> twitterAccounts = AccountStore.Create().FindAccountsForService("Twitter");
            foreach (var x in twitterAccounts)
            {
                twitterAccount = x;
                hasStoredAccout = true;
            }	

            if (!hasStoredAccout)
            {
                TwitterLoginPost(clientID, secrete, message, link);
            }
            else
            {
                TwitterPost(twitterAccount, message, clientID, secrete, link);
            }
        }

        private void TwitterLoginPost(string clientID, string secrete, string message, string link)
        { 
            var auth = new OAuth1Authenticator(
                           consumerKey: clientID,
                           consumerSecret: secrete,
                           requestTokenUrl: new Uri("https://api.twitter.com/oauth/request_token"),
                           authorizeUrl: new Uri("https://api.twitter.com/oauth/authorize"),
                           accessTokenUrl: new Uri("https://api.twitter.com/oauth/access_token"),
                           callbackUrl: new Uri("http://www.facebook.com/connect/login_success.html")
                       );

            auth.Completed += (sender, eventArgs) =>
            {
                UIApplication.SharedApplication.KeyWindow.RootViewController.DismissViewController(true, () =>
                    {
                        if (eventArgs.IsAuthenticated)
                        {
                            Console.WriteLine(eventArgs.Account.ToString());

                            //save the account
                            AccountStore.Create().Save(eventArgs.Account, "Twitter");
                            //post the message
                            TwitterPost(eventArgs.Account, message, clientID, secrete, link);
                        }
                        else
                        {
                            // The user cancelled during authentication.  do nothing

                        }
                    });
            };
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(auth.GetUI(), true, null);
        }


        private void TwitterPost(Account twitterAccount, string message, string clientID, string secrete, string link)
        {
			
            var request = new OAuth1Request("GET", new Uri("https://api.twitter.com/1.1/account/verify_credentials.json"), null, twitterAccount);
            request.GetResponseAsync().ContinueWith(t =>
                {
                    if (t.IsFaulted)
                    {
                        Console.WriteLine("Error: " + t.Exception.InnerException.Message);
                        TwitterLoginPost(clientID, secrete, message, link);
                    }
                    else
                    {
                        // 1. Create the service
                        var twitter = new TwitterService
                        {
                            ConsumerKey = clientID,
                            ConsumerSecret = secrete
                        };

                        twitter.SaveAccount(twitterAccount);

                        // 2. Create an item to share
                        var item = new Item();
                        item.Text = message;
                        if (link != null)
                        {
                            item.Links.Add(new Uri(link));
                        }

                        // 3. Present the UI on iOS
                        InvokeOnMainThread(() =>
                            {
                                var shareController = twitter.GetShareUI(item, result =>
                                    {
                                        UIApplication.SharedApplication.KeyWindow.RootViewController.DismissViewController(true, null);
                                    });
                                UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(shareController, true, null);
                            });
                    }
                });
        }
    }
}

