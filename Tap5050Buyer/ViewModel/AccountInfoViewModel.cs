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
    }
}

