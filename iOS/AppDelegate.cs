using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Geolocation;

namespace Tap5050Buyer.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : XLabs.Forms.XFormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            if (!Resolver.IsSet)
            {
                SetIoc();
            }

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        private void SetIoc()
        {
            var resolverContainer = new SimpleContainer();

            resolverContainer.Register<IGeolocator>(new Geolocator())
                .Register<IDevice>(t => AppleDevice.CurrentDevice);

            Resolver.SetResolver(resolverContainer.GetResolver());
        }
    }
}

