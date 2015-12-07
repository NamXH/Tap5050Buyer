using System;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Tap5050Buyer
{
    public class AccountInfoViewModel
    {
        internal const string c_serverBaseAddress = "https://dev.tap5050.com/";
        internal const string c_userApiAddress = "apex/tap5050_dev/Mobile_Web_Serv/users";
        internal const string c_userUpdateApiAddress = "apex/tap5050_dev/Mobile_Web_Serv/user_update";

        public UserAccount UserAccount { get; set; }

        public UserAccount UserAccountCopy { get; set; }

        public string ConfirmEmail { get; set; }

        public AccountInfoViewModel()
        {
            UserAccount = new UserAccount();
        }

        public AccountInfoViewModel(UserAccount userAccount)
        {
            UserAccount = userAccount;
            UserAccountCopy = new UserAccount(UserAccount);
        }

        public bool InfoHasNotChanged()
        {
            if (UserAccountCopy == null)
            {
                return false;
            }
            else
            {
                return UserAccount.Equals(UserAccount, UserAccountCopy);
            }
        }

        public bool PhoneNumberHasNotChanged()
        {
            if (UserAccountCopy == null)
            {
                return false;
            }
            else
            {
                return UserAccount.Phone == UserAccountCopy.Phone;
            }
        }

        public async Task<Tuple<bool, string>> LoadAccountInfo() // Not the best design!!
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(c_serverBaseAddress);

                HttpResponseMessage response = null;
                try // should be more fine grain maybe !!
                {
                    if (DatabaseManager.Token == null)
                    {
                        throw new Exception("Token is null while trying to retrieve data!");
                    }

                    var result = await ServerCaller.ExtendTokenAsync(DatabaseManager.Token.Value);

                    if (result.Item1)
                    {
                        var url = c_userApiAddress + "?token_id=" + DatabaseManager.Token.Value;
                        response = await client.GetAsync(url);

                        if (response.IsSuccessStatusCode)
                        {
                            var json = response.Content.ReadAsStringAsync().Result;

                            UserAccount = JsonConvert.DeserializeObject<UserAccount>(json);

                            // Translate from code to full name
                            var country = DatabaseManager.DbConnection.Table<Country>().Where(x => x.CountryCode == UserAccount.CountryCode).FirstOrDefault();
                            if (country != null)
                            {
                                UserAccount.Country = country.CountryName;
                            }

                            var province = DatabaseManager.DbConnection.Table<Province>().Where(x => x.ProvinceAbbreviation == UserAccount.ProvinceAbbreviation).FirstOrDefault();
                            if (province != null)
                            {
                                UserAccount.Province = province.ProvinceName;
                            }

                            return new Tuple<bool, string>(true, "Success");
                        }
                        else
                        {
                            // Handle unsuccessful requests!!
                            return new Tuple<bool, string>(false, "Cannot retrieve account info.");
                        }
                    }
                    else
                    {
                        // Expired token
                        DatabaseManager.DeleteToken();
                        MessagingCenter.Send<AccountInfoViewModel>(this, "Token Deleted");
                        return new Tuple<bool, string>(false, "Login token is invalid.");
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Error while retrieving account info: " + e.Message, e);
                }
            }
        }

        public async Task<Tuple<bool, string>> CreateAccount()
        {
            // Translate
            var country = DatabaseManager.DbConnection.Table<Country>().Where(x => x.CountryName == UserAccount.Country).FirstOrDefault();
            if (country != null)
            {
                UserAccount.CountryCode = country.CountryCode;
            }
            else
            {
                UserAccount.CountryCode = "";
            }
            var province = DatabaseManager.DbConnection.Table<Province>().Where(x => x.ProvinceName == UserAccount.Province).FirstOrDefault();
            if (province != null)
            {
                UserAccount.ProvinceAbbreviation = province.ProvinceAbbreviation;
            }
            else
            {
                UserAccount.ProvinceAbbreviation = "";
            }
                
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(c_serverBaseAddress);

                var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("email", UserAccount.Email),
                        new KeyValuePair<string, string>("firstname", UserAccount.FirstName),
                        new KeyValuePair<string, string>("lastname", UserAccount.LastName),
                        new KeyValuePair<string, string>("birthdate", UserAccount.Birthday.ToString("yyyy/mm/dd")),
                        new KeyValuePair<string, string>("m_phone", UserAccount.Phone),
                        new KeyValuePair<string, string>("add1", UserAccount.AddressLine1),
                        new KeyValuePair<string, string>("add2", UserAccount.AddressLine2),
                        new KeyValuePair<string, string>("city", UserAccount.City),
                        new KeyValuePair<string, string>("province", UserAccount.ProvinceAbbreviation),
                        new KeyValuePair<string, string>("postal", UserAccount.PostalCode),
                        new KeyValuePair<string, string>("country", UserAccount.CountryCode),
                        new KeyValuePair<string, string>("raffle_result", UserAccount.PreferedContactMethod),
                        new KeyValuePair<string, string>("charity_marketing_message", UserAccount.PreferedContactMethodCharity),
                    });

                HttpResponseMessage response = null;
                try
                {
                    response = await client.PostAsync(c_userApiAddress, content);
                }
                catch (Exception e)
                {
                    throw new Exception("Error while creating new account!", e);
                }
                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;

                    var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                    string successCode;

                    if (values.TryGetValue("result_success", out successCode))
                    {
                        if (successCode == "Y")
                        {
                            return new Tuple<bool, string>(true, String.Empty);
                        }
                        else
                        {
                            return new Tuple<bool, string>(true, String.Empty);  // Change to True for test when needed!!
                        }
                    }
                    else
                    {
                        throw new Exception("Error parsing server's json!"); 
                    } 
                }
                else
                {
                    return new Tuple<bool, string>(false, response.ReasonPhrase);
                }
            } 
        }

        public async Task<Tuple<bool, string>> UpdateAccountInfo()
        {
            // Translate
            var country = DatabaseManager.DbConnection.Table<Country>().Where(x => x.CountryName == UserAccount.Country).FirstOrDefault();
            if (country != null)
            {
                UserAccount.CountryCode = country.CountryCode;
            }
            else
            {
                UserAccount.CountryCode = "";
            }
            var province = DatabaseManager.DbConnection.Table<Province>().Where(x => x.ProvinceName == UserAccount.Province).FirstOrDefault();
            if (province != null)
            {
                UserAccount.ProvinceAbbreviation = province.ProvinceAbbreviation;
            }
            else
            {
                UserAccount.ProvinceAbbreviation = "";
            }

            var queryString = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("token_id", DatabaseManager.Token.Value)
            };

            var body = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("email", UserAccount.Email),
                new KeyValuePair<string, string>("firstname", UserAccount.FirstName),
                new KeyValuePair<string, string>("lastname", UserAccount.LastName),
                new KeyValuePair<string, string>("birthdate", UserAccount.BirthdayServerCallsFormat),
                new KeyValuePair<string, string>("m_phone", UserAccount.Phone),
                new KeyValuePair<string, string>("add1", UserAccount.AddressLine1),
                new KeyValuePair<string, string>("add2", UserAccount.AddressLine2),
                new KeyValuePair<string, string>("city", UserAccount.City),
                new KeyValuePair<string, string>("province", UserAccount.ProvinceAbbreviation),
                new KeyValuePair<string, string>("postal", UserAccount.PostalCode),
                new KeyValuePair<string, string>("country", UserAccount.CountryCode),
                new KeyValuePair<string, string>("raffle_result", UserAccount.PreferedContactMethod),
                new KeyValuePair<string, string>("charity_marketing_message", UserAccount.PreferedContactMethodCharity),
            };

            return await ServerCaller.PostAsync(queryString, body, c_userUpdateApiAddress);
        }

        public void SignOut()
        {
            DatabaseManager.DeleteToken();
            MessagingCenter.Send<AccountInfoViewModel>(this, "Logout");
        }
    }
}

