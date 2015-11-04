using System;
using System.Threading.Tasks;

namespace Tap5050Buyer
{
    public interface IWebRequestProtocolVersion10
    {
        /// <summary>
        /// Create an HttpWebRequest with ProtocolVersion = HttpVersion.Version10.
        /// Use this since ProtocolVersion is not PCL compatible.
        /// </summary>
        /// <param name="uri">URI.</param>
        Task<string> GetResponseStringAsync(string uri);
    }
}