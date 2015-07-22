using System;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Tap5050Buyer
{
    public class AccountInfoViewModel
    {
        internal const string c_serverBaseAddress = "http://dev.tap5050.com/";
        internal const string c_userApiAddress = "apex/tap5050_dev/Mobile_Web_Serv/users";

        public UserAccount UserAccount { get; set; }

        public string ConfirmEmail { get; set; }

        public AccountInfoViewModel()
        {
            UserAccount = new UserAccount();
        }

        public async Task GetAccountInfo()
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

                    var url = c_userApiAddress + "?token_id=" + DatabaseManager.Token.Value;
                    response = await client.GetAsync(url);

                    var json = response.Content.ReadAsStringAsync().Result;

                    UserAccount = JsonConvert.DeserializeObject<UserAccount>(json);
                }
                catch (Exception e)
                {
                    throw new Exception("Error while retrieving account info:" + e.Message);
                }
            }
        }

        public async Task<Tuple<bool, string>> CreateAccount()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(c_serverBaseAddress);

                var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("email", UserAccount.Email),
                        new KeyValuePair<string, string>("firstname", UserAccount.FirstName),
                        new KeyValuePair<string, string>("lastname", UserAccount.LastName),
                        new KeyValuePair<string, string>("birthdate", UserAccount.Birthday.ToString("yyyy/mm/dd")),
                        new KeyValuePair<string, string>("m_phone", UserAccount.FirstName),
                        new KeyValuePair<string, string>("add1", UserAccount.FirstName),
                        new KeyValuePair<string, string>("add2", UserAccount.FirstName),
                        new KeyValuePair<string, string>("city", UserAccount.FirstName),
                        new KeyValuePair<string, string>("province", UserAccount.FirstName),
                        new KeyValuePair<string, string>("postal", UserAccount.FirstName),
                        new KeyValuePair<string, string>("country", UserAccount.FirstName),
                        new KeyValuePair<string, string>("raffle_result", UserAccount.FirstName),
                        new KeyValuePair<string, string>("charity_marketing_message", UserAccount.FirstName),
                    });

                HttpResponseMessage response = null;
                try
                {
                    response = await client.PostAsync(c_userApiAddress, content);
                }
                catch (Exception)
                {
                    throw new Exception("Error while creating new account!");
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


    }
}

