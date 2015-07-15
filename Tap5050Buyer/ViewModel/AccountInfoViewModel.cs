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
        }

        public async Task GetAccountInfo()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(c_serverBaseAddress);

                HttpResponseMessage response = null;
                try // should be splited !!
                {
                    var url = c_userApiAddress + "?token_id=" + DatabaseManager.Token;
                    response = await client.GetAsync(url);

                    var json = response.Content.ReadAsStringAsync().Result;

                    UserAccount = JsonConvert.DeserializeObject<UserAccount>(json);
                }
                catch (Exception)
                {
                    throw new Exception("Error while retrieving account info!");
                }
            }
        }

        public async Task CreateAccount()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(c_serverBaseAddress);

                var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("email", UserAccount.Email),
                        new KeyValuePair<string, string>("firstname", UserAccount.FirstName),
                        new KeyValuePair<string, string>("lastname", UserAccount.LastName),
                        new KeyValuePair<string, string>("birthdate", UserAccount.YearOfBirth + "/" + UserAccount.MonthOfBirth + "/" + UserAccount.DateOfBirth), // need changed later
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
                try // should be splited !!
                {
                    var url = c_userApiAddress + "?token_id=" + DatabaseManager.Token;
                    response = await client.GetAsync(url);

                    var json = response.Content.ReadAsStringAsync().Result;

                    UserAccount = JsonConvert.DeserializeObject<UserAccount>(json);
                }
                catch (Exception)
                {
                    throw new Exception("Error while retrieving account info!");
                }
            } 
        }
    }
}

