using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Contacts.Plugin.Abstractions;
using Xamarin.Forms;

namespace Tap5050Buyer
{
    public class ContactsViewModel
    {
        private const string c_smsMessageTemplate = "I am supporting {0} and I hope you will too by purchasing a raffle ticket at {1} if you are in {2}";
        private static readonly string c_emailMessageTemplate = "Hey, I’m supporting {0} and it would be great if you supported them too. If you’re interested in helping, you can buy a raffle ticket at {1}. It’s a great cause and I thought you may be interested." + Environment.NewLine + Environment.NewLine + "BTW it’s only available for people that live in {2}." + Environment.NewLine + Environment.NewLine + "Talk soon,";

        public ObservableCollection<ExtendedContact> ExtendedContacts { get; set; }

        public RaffleEvent Raffle { get; set; }

        public ContactsViewModel(IList<ExtendedContact> extendedContact, RaffleEvent raffle)
        {
            ExtendedContacts = new ObservableCollection<ExtendedContact>(extendedContact.OrderBy(x => x.InnerContact.DisplayName).ToList());
            if (raffle != null)
            {
                Raffle = raffle;
            }
            else
            {
                Raffle = new RaffleEvent();
            }
        }

        public void SendSms()
        {
            var socialShare = DependencyService.Get<ISocialShare>();

            var selectedContactNumbers = new List<string>();
            foreach (var extendedContact in ExtendedContacts)
            {
                if (extendedContact.IsSelected)
                {
                    selectedContactNumbers.Add(extendedContact.InnerContact.Phones.FirstOrDefault(x => x.Label.Equals("Mobile", StringComparison.OrdinalIgnoreCase)).Number);
                }
            }

            socialShare.Sms(String.Format(c_smsMessageTemplate, Raffle.Organization, Raffle.BuyTicketUrl, Raffle.LocationName), selectedContactNumbers.ToArray());

            MessagingCenter.Send<ContactsViewModel>(this, "Done");
        }

        public void SendEmails()
        {
            var socialShare = DependencyService.Get<ISocialShare>(); 

            var selectedContactNumbers = new List<string>();
            foreach (var extendedContact in ExtendedContacts)
            {
                if (extendedContact.IsSelected)
                {
                    selectedContactNumbers.Add(extendedContact.InnerContact.Emails.FirstOrDefault().Address);
                }
            }

            socialShare.Email(String.Format(c_emailMessageTemplate, Raffle.Organization, Raffle.BuyTicketUrl, Raffle.LocationName), Raffle.Organization, selectedContactNumbers.ToArray());

            MessagingCenter.Send<ContactsViewModel>(this, "Done");
        }
    }
}