using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Tap5050Buyer
{
    public class VerifyPhoneNumberViewModel
    {
        internal const string c_serverBaseAddress = "https://www.tap5050.com/";
        internal const string c_requestPhoneVerificationApiAddress = "apex/tap5050/Mobile_Web_Serv/request_mobile_phone_verification";
        internal const string c_phoneVerificationApiAddress = "apex/tap5050/Mobile_Web_Serv/mobile_phone_verification";

        public string VerificationCode { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Country { get; set; }

        public VerifyPhoneNumberViewModel(string email, string phoneNumber, string countryCode)
        {
            Email = email;
            PhoneNumber = phoneNumber;
            Country = countryCode;
        }

        public async Task<Tuple<bool, string>> RequestPhoneNumberVerification()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(c_serverBaseAddress);

                // No need to check token expiration here since it has just been checked in AccountInfoVM, TicketListVM
                var url = c_requestPhoneVerificationApiAddress + "?token_id=" + DatabaseManager.Token.Value;

                HttpResponseMessage response = null;
                try
                {
                    response = await client.PostAsync(url, new StringContent(""));
                }
                catch (Exception e)
                {
                    return new Tuple<bool, string>(false, e.Message);
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
                            return new Tuple<bool, string>(true, "Success");
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

        public async Task<Tuple<bool, string>> VerifyPhoneNumber()
        {
            var body = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("email", Email),
                new KeyValuePair<string, string>("m_phone", PhoneNumber),
                new KeyValuePair<string, string>("VERIFY_CODE", VerificationCode)
            };
            return await ServerCaller.PostAsync(null, body, c_phoneVerificationApiAddress);
        }
    }
}

