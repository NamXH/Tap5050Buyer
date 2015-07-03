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
        public ObservableCollection<ExtendedContact> ExtendedContacts { get; set; }

        public ContactsViewModel(IList<ExtendedContact> extendedContact)
        {
            ExtendedContacts = new ObservableCollection<ExtendedContact>(extendedContact.OrderBy(x => x.InnerContact.DisplayName).ToList());
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

            socialShare.Sms("Testing", selectedContactNumbers.ToArray());
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

            socialShare.Email("Testing", "abc", selectedContactNumbers.ToArray());
        }
    }
}