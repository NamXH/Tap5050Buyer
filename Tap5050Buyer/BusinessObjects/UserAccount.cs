using System;
using Newtonsoft.Json;

namespace Tap5050Buyer
{
    public class UserAccount
    {
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName 
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "last_name")]
        public string LastName
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "user_birthdate")]
        public DateTime Birthday
        {
            get;
            set;
        }

        public string BirthdayShortFormat
        {
            get
            {
                return Birthday.ToString("d");
            }
        }

        public string DateOfBirth
        {
            get;
            set;
        }

        public string MonthOfBirth
        {
            get;
            set;
        }

        public string YearOfBirth 
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "phone_mobile")]
        public string Phone
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "email")]
        public string Email
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "mail_address_line_1")]
        public string AddressLine1
        {
            get;
            set;
        }

        public string AddressLine2
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "mail_country")]
        public string Country
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "mail_prov_state")]
        public string Province
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "mail_city")]
        public string City
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "mail_postal")]
        public string PostalCode
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "prefered_contact_method")]
        public string PreferedContactMethod
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "prefered_contact_methodcharity")]
        public string PreferedContactMethodcharity
        {
            get;
            set;
        }

        public UserAccount()
        {
            PreferedContactMethod = "NOCONTACT";

            PreferedContactMethodcharity = "NOCONTACT";
        }
    }
}

