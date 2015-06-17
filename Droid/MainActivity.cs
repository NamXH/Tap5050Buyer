using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using XLabs.Ioc;
using XLabs.Platform.Services.Geolocation;

namespace Tap5050Buyer.Droid
{
    [Activity(Label = "Tap5050Buyer.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, Theme = "@android:style/Theme.Holo.Light")]
    public class MainActivity : XLabs.Forms.XFormsApplicationDroid
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            if (!Resolver.IsSet)
            {
                SetIoc();
            }

            LoadApplication(new App());
        }

        private void SetIoc()
        {
            var resolverContainer = new SimpleContainer();

            resolverContainer.Register<IGeolocator, Geolocator>();

            Resolver.SetResolver(resolverContainer.GetResolver());
        }
    }
}

