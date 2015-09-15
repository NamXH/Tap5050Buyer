using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Tap5050Buyer
{
    public static class ServerCaller
    {
        // May remove the trailing slash later!!
        public static readonly string ServerBaseAddress = "https://dev.tap5050.com/";

        public static async Task<Tuple<bool, string>> PostAsync(List<KeyValuePair<string, string>> queryString, List<KeyValuePair<string, string>> body, string endpointUrl)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ServerBaseAddress);

                var content = new FormUrlEncodedContent(body);
                if (queryString != null)
                {
                    endpointUrl += "?";
                    foreach (var kvp in queryString)
                    {
                        endpointUrl += String.Format("{0}={1}&", kvp.Key, kvp.Value);
                    }
                    endpointUrl.Remove(endpointUrl.Length - 1); //Remove the extra &
                }

                HttpResponseMessage response = null;
                try
                {
                    response = await client.PostAsync(endpointUrl, content);
                }
                catch (Exception e)
                {
                    throw new Exception(String.Format("Error when sending Post request to {0}{1}: {2}", ServerBaseAddress, endpointUrl, e.Message), e);
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
                                throw new Exception("Error parsing server's json: " + json); 
                            } 
                        }
                    }
                    else
                    {
                        throw new Exception("Error parsing server's json: " + json); 
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

