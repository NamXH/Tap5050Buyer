using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using Xamarin;

namespace Tap5050Buyer.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.

//            Insights.Initialize(Insights.DebugModeKey);
            Insights.Initialize("0b1f83db0942cb019d5dc57abfe89464310133c8");
            Insights.Track("iOS Starts");

            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}