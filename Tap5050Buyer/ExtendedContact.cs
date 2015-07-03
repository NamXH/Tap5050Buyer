using System;
using Contacts.Plugin.Abstractions;

namespace Tap5050Buyer
{
    public class ExtendedContact
    {
        public Contact InnerContact { get; set; }

        public bool IsSelected { get; set; }

        public ExtendedContact(Contact contact)
        {
            InnerContact = contact;
            IsSelected = false;
        }
    }
}