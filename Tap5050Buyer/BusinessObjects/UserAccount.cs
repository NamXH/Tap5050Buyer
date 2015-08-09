using System;
using Newtonsoft.Json;
using System.Diagnostics;

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

        [JsonIgnore]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        [JsonProperty(PropertyName = "user_birthdate")]
        public DateTime Birthday
        {
            get;
            set;
        }

        [JsonIgnore]
        public string BirthdayShortFormat
        {
            get
            {
                return Birthday.ToString("MMMM dd, yyyy");
            }
        }

        public string BirthdayServerCallsFormat
        {
            get
            {
                return Birthday.ToString("yyyy/MM/dd");
            }
        }

        [JsonIgnore]
        public string DateOfBirth
        {
            get;
            set;
        }

        [JsonIgnore]
        public string MonthOfBirth
        {
            get;
            set;
        }

        [JsonIgnore]
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

        [JsonProperty(PropertyName = "mail_address_line_2")]
        public string AddressLine2
        {
            get;
            set;
        }

        [JsonIgnore]
        public string StreetAddress
        {
            get
            {
                return AddressLine1 + ", " + AddressLine2;
            }
        }

        // Country and Country code are only different when:
        // + Getting Account Info from server: CountryCode is filled -> derive Country (null at first)
        // + Registering new Account: Country is filled -> derive Country Code (null at first)
        // + Updating Account Info: Country is filled -> derive Country Code (old valude at first)
        [JsonIgnore]
        public string Country
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "mail_country")]
        public string CountryCode
        {
            get;
            set;
        }

        [JsonIgnore]
        public string Province
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "mail_prov_state")]
        public string ProvinceAbbreviation
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

        [JsonIgnore]
        public string CityAddress
        {
            get
            {
                return City + ", " + Province + " " + PostalCode;
            }
        }

        [JsonProperty(PropertyName = "prefered_contact_method")]
        public string PreferedContactMethod
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "prefered_contact_methodcharity")]
        public string PreferedContactMethodCharity
        {
            get;
            set;
        }

        public UserAccount()
        {
            Birthday = new DateTime(1900, 1, 1);
            PreferedContactMethod = "NOCONTACT";
            PreferedContactMethodCharity = "NOCONTACT";
        }

        public UserAccount(UserAccount anotherAccount)
        {
            Email = anotherAccount.Email;
            FirstName = anotherAccount.FirstName;
            LastName = anotherAccount.LastName;
            Birthday = anotherAccount.Birthday;
            Phone = anotherAccount.Phone;
            AddressLine1 = anotherAccount.AddressLine1;
            AddressLine2 = anotherAccount.AddressLine2;
            City = anotherAccount.City;
            Province = anotherAccount.Province;
            PostalCode = anotherAccount.PostalCode;
            Country = anotherAccount.Country;
            PreferedContactMethod = anotherAccount.PreferedContactMethod;
            PreferedContactMethodCharity = anotherAccount.PreferedContactMethodCharity;
        }

        // Should override Equals instead!! -> Later
        public static bool Equals(UserAccount first, UserAccount second)
        {
            var rs = (first.Email == second.Email)
                     && (first.FirstName == second.FirstName)
                     && (first.LastName == second.LastName)
                     && (first.Birthday.Date == second.Birthday.Date)// Ignore hour:minute is important. Date picker for some reasons use 12:00:00 AM while the default value after deserialization is 06:00:00 AM
                     && (first.Phone == second.Phone)
                     && (first.AddressLine1 == second.AddressLine1)
                     && (first.AddressLine2 == second.AddressLine2)
                     && (first.City == second.City)
                     && (first.Province == second.Province)
                     && (first.PostalCode == second.PostalCode)
                     && (first.Country == second.Country)
                     && (first.PreferedContactMethod == second.PreferedContactMethod)
                     && (first.PreferedContactMethodCharity == second.PreferedContactMethodCharity);

            return rs;
        }
    }
}

