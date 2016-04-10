using System;

namespace Tap5050Buyer
{
    public interface ISocialShare
    {
        void Facebook(string clientID, string message, string link, string imgUrl, string raffleName, string charityName);

        void Twitter(string clientID, string secrete, string message, string link);

        void Email(string message, string subject, string[] recivers);

        void Sms(string message, string[] recivers);

        void ResetLogin();
    }
}