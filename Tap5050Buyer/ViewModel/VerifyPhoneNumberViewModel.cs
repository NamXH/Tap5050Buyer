using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Tap5050Buyer
{
    public class VerifyPhoneNumberViewModel
    {
        internal const string c_serverBaseAddress = "http://dev.tap5050.com/";
        internal const string c_requestPhoneVerificationApiAddress = "apex/tap5050_dev/Mobile_Web_Serv/request_mobile_phone_verification";

        public string VerificationCode { get; set; }

        public VerifyPhoneNumberViewModel()
        {
        }

        public async Task<Tuple<bool, string>> RequestPhoneNumberVerification(string email, string phoneNumber, string country)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(c_serverBaseAddress);

                var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("email", email),
                        new KeyValuePair<string, string>("m_phone", phoneNumber),
                        new KeyValuePair<string, string>("country", country),
                    });

                HttpResponseMessage response = null;
                try
                {
                    response = await client.PostAsync(c_requestPhoneVerificationApiAddress, content);
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
    }
}

