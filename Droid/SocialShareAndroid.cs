using System;
using Xamarin.Auth;
using System.Collections.Generic;
using Xamarin.Social.Services;
using Xamarin.Social;
using Xamarin.Forms;
using Android.Content;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Provider;
using Tap5050Buyer.Droid;

[assembly: Xamarin.Forms.Dependency (typeof (SocialShareAndroid))]
namespace Tap5050Buyer.Droid
{
	public class SocialShareAndroid: Activity, SocialShare
	{
		public SocialShareAndroid ()
		{
		}

		public void printConsoleString (string message)
		{
			Console.WriteLine (message);
		}

		public void resetLogin()
		{
			//delete all facebook accounts
			IEnumerable<Account> facebookAccounts = AccountStore.Create (Forms.Context).FindAccountsForService ("Facebook");
			foreach (var x in facebookAccounts) {
				AccountStore.Create (Forms.Context).Delete (x, "Facebook");
			}

			//delete all twitter accounts
			IEnumerable<Account> twitterAccounts = AccountStore.Create (Forms.Context).FindAccountsForService ("Twitter");
			foreach (var x in twitterAccounts) {
				AccountStore.Create (Forms.Context).Delete (x, "Twitter");
			}
		}	

		public void email(string message,string link,string subject,string [] recivers )
		{
			var email = new Intent (Android.Content.Intent.ActionSend);

			if (recivers != null) {
				email.PutExtra (Android.Content.Intent.ExtraEmail, recivers);
			}

			email.PutExtra (Android.Content.Intent.ExtraSubject, subject);

			email.PutExtra (Android.Content.Intent.ExtraText, message +"\n"+link);

			email.SetType ("message/rfc822");

			Forms.Context.StartActivity (email);
		}

		public void sms(string message,string link,string [] recivers )
		{
			string smsto="smsto:";
			if (recivers != null) {
				foreach (var x in recivers) {
					smsto+=x+",";
				}
				smsto = smsto.Substring (0, smsto.Length - 1);
			}	
			var smsUri = Android.Net.Uri.Parse(smsto);
			var smsIntent = new Intent (Intent.ActionSendto, smsUri);
			smsIntent.PutExtra ("sms_body", message +"\n"+link);  
			Forms.Context.StartActivity (smsIntent);
		}

		public void facebook (string clientID, string message,string link)
		{
			//check if there is a stored account
			Account facebookAccount = new Account (); 
			bool hasStoredAccout = false;
		
			IEnumerable<Account> facebookAccounts = AccountStore.Create (Forms.Context).FindAccountsForService ("Facebook");
			foreach (var x in facebookAccounts) {
				facebookAccount = x;
				hasStoredAccout = true;
			}	

			if (!hasStoredAccout) {
				facebookLoginPost (clientID,message,link);
			} else {
				facebookPost (facebookAccount, message, clientID,link);
			}
		}



		private void facebookLoginPost (string clientID,string message,string link)
		{
			var auth = new OAuth2Authenticator (
				clientId: clientID,
				scope: "publish_actions",
				authorizeUrl: new Uri ("https://m.facebook.com/dialog/oauth/"),
				redirectUrl: new Uri ("http://www.facebook.com/connect/login_success.html"));

			auth.Completed += (sender, eventArgs) => {
				if (eventArgs.IsAuthenticated) {
					//save the account
					AccountStore.Create (Forms.Context).Save (eventArgs.Account, "Facebook");
					//post the message
					facebookPost (eventArgs.Account, message, clientID,link);
				} else {
					// The user cancelled during authentication.  do nothing

				}
			};
			Forms.Context.StartActivity (auth.GetUI(Forms.Context));
		}
        

		private void facebookPost(Account facebookAccount,string message,string clientID,string link){
			var request = new OAuth2Request ("GET", new Uri ("https://graph.facebook.com/me"), null, facebookAccount);
			request.GetResponseAsync ().ContinueWith (t => {
				if (t.IsFaulted){
					Console.WriteLine ("Error: " + t.Exception.InnerException.Message);
					facebookLoginPost(clientID,message,link);
				}
				else {
					// 1. Create the service
					var facebook = new FacebookService { ClientId = clientID };

					facebook.SaveAccount(Forms.Context,facebookAccount);

					// 2. Create an item to share
					var item = new Item();
					item.Text=message;
					item.Links.Add (new Uri (link));
					Device.BeginInvokeOnMainThread(()=>{
						// 3. Present the UI on iOS
						var shareIntent = facebook.GetShareUI ((Activity)Forms.Context, item, result => {
							// result lets you know if the user shared the item or canceled
						});
						Forms.Context.StartActivity (shareIntent);
					});
				}		
			});
		}

		public void twitter (string clientID, string secrete,string message,string link)
		{
			//check if there is a stored account
			Account twitterAccount = new Account (); 
			bool hasStoredAccout = false;

			IEnumerable<Account> twitterAccounts = AccountStore.Create (Forms.Context).FindAccountsForService ("Twitter");
			foreach (var x in twitterAccounts) {
				twitterAccount = x;
				hasStoredAccout = true;
			}	

			if (!hasStoredAccout) {
				twitterLoginPost (clientID,secrete,message,link);
			} else {
				twitterPost (twitterAccount, message, clientID,secrete,link);
			}
		}

		private void twitterLoginPost (string clientID,string secrete,string message,string link)
		{ 
			var auth=new OAuth1Authenticator(
				consumerKey:clientID,
				consumerSecret:secrete,
				requestTokenUrl:new Uri ("https://api.twitter.com/oauth/request_token"),
				authorizeUrl:new Uri ("https://api.twitter.com/oauth/authorize"),
				accessTokenUrl:new Uri ("https://api.twitter.com/oauth/access_token"),
				callbackUrl:new Uri ("http://www.facebook.com/connect/login_success.html")
			);

			auth.Completed += (sender, eventArgs) => {
				if (eventArgs.IsAuthenticated) {
					//save the account
					AccountStore.Create (Forms.Context).Save (eventArgs.Account, "Twitter");
					//post the message
					twitterPost (eventArgs.Account, message, clientID,secrete,link);
				} else {
					// The user cancelled during authentication.  do nothing

				}
			};
			Forms.Context.StartActivity (auth.GetUI(Forms.Context));
		}


		private void twitterPost(Account twitterAccount,string message,string clientID,string secrete,string link){

			var request = new OAuth1Request ("GET", new Uri ("https://api.twitter.com/1.1/account/verify_credentials.json"), null, twitterAccount);
			request.GetResponseAsync ().ContinueWith (t => {
				if (t.IsFaulted){
					Console.WriteLine ("Error: " + t.Exception.InnerException.Message);
					twitterLoginPost (clientID,secrete,message,link);
				}
				else {
					// 1. Create the service
					var twitter = new TwitterService {
						ConsumerKey = clientID,
						ConsumerSecret = secrete
					};

					twitter.SaveAccount (Forms.Context,twitterAccount);

					// 2. Create an item to share
					var item = new Item();
					item.Text=message;
					item.Links.Add (new Uri (link));

					// 3. Present the UI 
					Device.BeginInvokeOnMainThread(()=>{
						// 3. Present the UI on iOS
						var shareIntent = twitter.GetShareUI ((Activity)Forms.Context, item, result => {
							// result lets you know if the user shared the item or canceled
						});
						Forms.Context.StartActivity (shareIntent);
					});
				}
			});
		}
	}
}

