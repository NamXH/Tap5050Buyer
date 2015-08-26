using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace Tap5050Buyer
{
    public class LoginPageViewModel
    {
        internal const string c_serverBaseAddress = "https://dev.tap5050.com/";
        internal const string c_authenticationApiAddress = "apex/tap5050_dev/Mobile_Web_Serv/auth";
        internal const string c_resetPasswordApiAddress = "apex/tap5050_dev/Mobile_Web_Serv/reset_password";

        public string Username { get; set; }

        public string Password { get; set; }

        public async Task<Tuple<bool, string>> Login()
        {
            var rs = await Login(Username, Password);
            return rs;
        }

        public async Task<Tuple<bool, string>> Login(string username, string password)
        {
            if ((String.IsNullOrWhiteSpace(username)) && (String.IsNullOrWhiteSpace(password)))
            {
                throw new ArgumentNullException("Username or password is empty!");
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(c_serverBaseAddress);

                var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("username", username),
                        new KeyValuePair<string, string>("password", password),
                    });
                    
                HttpResponseMessage response = null;
                try
                {
                    response = await client.PostAsync(c_authenticationApiAddress, content);
                }
                catch (Exception e)
                {
                    throw new Exception("Error when authenticate:" + e.Message);
                }

                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;

                    var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                    string successCode;

                    if (values.TryGetValue("loginsuccess", out successCode))
                    {
                        if (successCode == "Y")
                        {
                            string tokenValue;
                            if (values.TryGetValue("token_id", out tokenValue))
                            {
                                DatabaseManager.InsertToken(tokenValue);
                                MessagingCenter.Send<LoginPageViewModel>(this, "Login");
                                
                                return new Tuple<bool, string>(true, tokenValue);
                            }
                            else
                            {
                                throw new Exception("Error parsing server's json!"); 
                            }  
                        }
                        else
                        {
                            string errorMessage;
                            if (values.TryGetValue("err_message", out errorMessage))
                            {
                                return new Tuple<bool, string>(false, errorMessage);
                            }
                            else
                            {
                                throw new Exception("Error parsing server's json!"); 
                            }
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

        public async Task<bool> ResetPassword(string username)
        {
            if (String.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException("Username is empty!");
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(c_serverBaseAddress);

                var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("email", username),
                    });

                HttpResponseMessage response = null;
                try
                {
                    response = await client.PostAsync(c_resetPasswordApiAddress, content);
                }
                catch (Exception e)
                {
                    throw new Exception("Error while resetting password :" + e.Message);
                }

                // BAD server api leads to this complexity!!
                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;

                    var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                    string successCode;

                    if (values.TryGetValue("result_success", out successCode))
                    {
                        if (successCode == "Y")
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public LoginPageViewModel()
        {
        }
    }
}