using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin;
using XLabs.Ioc;
using XLabs.Platform.Services.Geolocation;
using XLabs.Platform.Device;

namespace Tap5050Buyer.Droid
{
    [Activity(Label = "RaffleWallet", Icon = "@drawable/icon", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, Theme = "@android:style/Theme.Holo.Light")]
    public class MainActivity : XLabs.Forms.XFormsApplicationDroid
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

//            Insights.Initialize(Insights.DebugModeKey, (Android.App.Activity)Xamarin.Forms.Forms.Context);
            Insights.Initialize("0b1f83db0942cb019d5dc57abfe89464310133c8", (Android.App.Activity)Xamarin.Forms.Forms.Context);
            Insights.Track("Android start");

            if (!Resolver.IsSet)
            {
                SetIoc();
            }

            LoadApplication(new App());
        }

        private void SetIoc()
        {
            var resolverContainer = new SimpleContainer();

            resolverContainer.Register<IGeolocator, Geolocator>()
                .Register<IDevice>(t => AndroidDevice.CurrentDevice);

            Resolver.SetResolver(resolverContainer.GetResolver());
        }
    }
}

