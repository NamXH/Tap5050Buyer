using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Tap5050Buyer
{
    public partial class InviteFriendsPage : ContentPage
    {
		// Facebook and Twiiter account info
		readonly string facebookAppID = "483558705129715";
		readonly string twitterAPIKey="qO4EvQmukCLDch7YTQUAinGXf";
		readonly string twitterSecrete="rYPx1avhVxgJ5EbU5OjmbpVgSKWxCAaG4nOTDagOqEB7U9KxxT";

		/*
		string facebookAppID = "838514282900661";
		string twitterAPIKey="450048484-kvoIP6TlH4TPRDZas34SqRvsRMbIeH319ahu2lcl";
		string twitterSecrete="kLAHaxeDf8CPTDBYUjanu6FpogRsMX8qh5f39pidizTaF";
		*/


		/****************************************************************************
		* These are dummy data, you should remove or comments out these 
		* **************************************************************************/

		string buyTicketlink="http://dev.tap5050.com/apex/f?p=102:TICKETS:0::NO:::";
		string organizationName = "Main Testing Organization";
		string raffleName="Chris Time Zone Event";
		string location = "Saskatchewan";
		string emailSubject="emailTest";
		string[] emailRecivers = { "chh990@mail.usask.ca", "chihyi.t.huang@hotmail.com.tw" };
		string[] smsRecivers = { "13062612825","13068817772"};
		string testmessage="Ignore this message, I am testing my app. Sorry to bother you.";
		/*****************************************************************************/

        public InviteFriendsPage()
        {
            InitializeComponent();

			//thes button is used to reset the token stored in the device
			//you can remove this.
			Button resetLoginButton=new Button
			{
				Text = "Reset Username &Password!",
				Font = Font.SystemFontOfSize(NamedSize.Large),
				BorderWidth = 1,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			resetLoginButton.Clicked += (sender, e) => {
				DependencyService.Get<SocialShare> ().resetLogin ();
			};

			Button facebookButton = new Button
			{
				Text = "Post on facebook!",
				Font = Font.SystemFontOfSize(NamedSize.Large),
				BorderWidth = 1,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			facebookButton.Clicked   += (sender, e) => {
				//TODO: change the following line to your own 
				// all you have to change is the organizationName,raffleName,location and buyTicketlink.
				DependencyService.Get<SocialShare>().facebook(facebookAppID,getMessage(organizationName,raffleName,location),buyTicketlink);
			};

			Button twitterButton = new Button
			{
				Text = "Post on twitter!",
				Font = Font.SystemFontOfSize(NamedSize.Large),
				BorderWidth = 1,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			twitterButton.Clicked   += (sender, e) => {
				//TODO: change the following line to your own 
				// all you have to change is the organizationName,raffleName,location and buyTicketlink.
				DependencyService.Get<SocialShare>().twitter(twitterAPIKey,twitterSecrete,getMessage(organizationName,raffleName,location),buyTicketlink);
			};


			Button emailButton = new Button
			{
				Text = "Sent Email!",
				Font = Font.SystemFontOfSize(NamedSize.Large),
				BorderWidth = 1,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			emailButton.Clicked   += (sender, e) => {
				//TODO: change the following line to your own 
				//set emailRecivers to null if there is none
				DependencyService.Get<SocialShare>().email(getMessage(organizationName,raffleName,location),buyTicketlink,emailSubject,emailRecivers);
			};

			Button SMSButton = new Button
			{
				Text = "Sent SMS!",
				Font = Font.SystemFontOfSize(NamedSize.Large),
				BorderWidth = 1,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			SMSButton.Clicked   += (sender, e) => {
				//TODO: change the following line to your own 
				//set smsRecivers to null if there is none
				DependencyService.Get<SocialShare>().sms(testmessage,buyTicketlink,smsRecivers);
			};

			var sharePage =new ContentPage {
				Content = new StackLayout {
					VerticalOptions = LayoutOptions.Center,
					Children = {
						facebookButton,twitterButton,emailButton,SMSButton,resetLoginButton 
					}
				}
			};

			var layout = new StackLayout();
			this.Content = layout;
			var button = new Button();
			button.Text = "Share";
			layout.Children.Add(button);
			button.Clicked += (object sender, EventArgs e) => {
				this.Navigation.PushAsync(sharePage);
			};
		}

		public string getMessage(string organizationName,string raffleName,string location){
			return "Support "+organizationName+" by following the link below.\n ("+location+" only)\n";
		}
    }

	public interface SocialShare
	{
		void facebook(string clientID,string message,string link);
		void twitter(string clientID,string secrete,string message,string link);
		void email(string message,string link,string subject,string [] recivers );
		void sms(string message,string link,string [] recivers );
		void resetLogin();
	}
}