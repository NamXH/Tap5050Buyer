using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Tap5050Buyer
{
    public partial class InviteFriendsPage : ContentPage
    {
		// Facebook and Twiiter account info
		//readonly string facebookAppID = "483558705129715"; //Terence
		readonly string facebookAppID = "838514282900661"; //Chris
		readonly string twitterAPIKey="BDXgNpLtpotOT4vFqXLbg8cWE";
		readonly string twitterSecrete="JGoVsaWvqpJS6HdVPlSVZ2tYloFaXDni7TFWhfthL1LJrsAgh7";

		/****************************************************************************
		* These are dummy data, you should remove or comments out these 
		* **************************************************************************/

		string shortenURL="http://dev.tap5050.com/apex/f?p=102:TICKETS:0::NO:::";
		string organizationName = "Main Testing Organization";
		string raffleName="Chris Time Zone Event";

		string[] emailRecivers = { "chh990@mail.usask.ca", "chihyi.t.huang@hotmail.com.tw" };
		string[] smsRecivers = { "13062612825","13068817772"};
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
				DependencyService.Get<SocialShare>().facebook(facebookAppID,getFacebookBody(raffleName,organizationName),shortenURL);
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
				DependencyService.Get<SocialShare>().twitter(twitterAPIKey,twitterSecrete,getTwitterBody(raffleName,organizationName),shortenURL);

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
				DependencyService.Get<SocialShare>().email(getEmailBody(raffleName,organizationName,shortenURL),getEmailSubject(organizationName),emailRecivers);
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
				DependencyService.Get<SocialShare>().sms(getSMSBody(raffleName,organizationName,shortenURL),smsRecivers);
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


			
		public string getFacebookBody(string raffleName,string organizationName){
			return "I bought a "+raffleName+" ticket from "+organizationName+" to support the good they do in our community. If you would like a chance to win a great prize and support their efforts you can buy a ticket at ";
		}

		public string getTwitterBody(string raffleName,string organizationName){
			return "I just bought a raffle ticket to support "+organizationName+". To help out buy a ticket at ";
		}

		public string getEmailSubject(string organizationName){
			return "I am supporting" + organizationName;
		}	

		public string getEmailBody(string raffleName,string organizationName,string shortenURL){
			return "I bought a "+raffleName+" ticket from "+organizationName+" to support the good they do in our community. If you would like a chance to win a great prize and support their efforts you can buy a ticket at "+shortenURL+".";
		}

		public string getSMSBody(string raffleName,string organizationName,string shortenURL){
			return "I just bought a raffle ticket to support " + organizationName + ". You can help out too by purchasing a ticket at " + shortenURL + ".";
		}
    }

	public interface SocialShare
	{
		void facebook(string clientID,string message,string link);
		void twitter(string clientID,string secrete,string message,string link);
		void email(string message,string subject,string [] recivers );
		void sms(string message,string [] recivers );
		void resetLogin();
	}
}