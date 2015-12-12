using System;
using System.Net;
using Tap5050Buyer.iOS;
using System.Threading.Tasks;
using System.Text;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(WebRequestProtocolVersion10))]
namespace Tap5050Buyer.iOS
{
    public class WebRequestProtocolVersion10 : IWebRequestProtocolVersion10
    {
        public WebRequestProtocolVersion10()
        {
        }

        public async Task<string> GetResponseStringAsync(string uri)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "GET";
            request.ProtocolVersion = HttpVersion.Version10;

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, null);
            }
            catch
            {
                return String.Empty;
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                StringBuilder stringBuilder = new StringBuilder(); 
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null) // Follow Nan Chen's Tap5050Seller app solution
                    {
                        stringBuilder.Append(line);
                    }
                }
                var responseString = stringBuilder.ToString();
                return responseString;
            }
            else
            {
                return String.Empty;
            }
        }
    }
}