using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace Tap5050Buyer
{
    public partial class LoadingLocationPage : ContentPage
    {
        internal const string c_loadingMessage = "Waiting for your current location.";
        internal const string c_cannotReachServerErrorMessage = "There is a problem contacting server. Please check your Internet connection and try again later.";
        internal const string c_cannotDetectGeolocationMessage = "Cannot detect your location. Please manually pick one.";

        private LoadingLocationViewModel _viewModel;

        public LoadingLocationPage()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Back");

            _viewModel = new LoadingLocationViewModel();
            BindingContext = _viewModel;

            AddActivityIndicator();

            LoadInfo();
        }

        public async void LoadInfo()
        {
            await _viewModel.GetCurrentLocationAndServerData();

            if ((LoadingLocationViewModel.RaffleLocations == null) || (LoadingLocationViewModel.FailToGetCountriesOrProvinces))
            {
                RemoveAllElement();
                AddTryAgainButton();
            }
            else
            {
                LoadingLocationViewModel.IsLocationDetected = false; // For TEST!!
                if (LoadingLocationViewModel.IsLocationDetected)
                {
                    MessagingCenter.Send<LoadingLocationPage>(this, "Success");
                }
                else
                {
                    RemoveAllElement();
                    AddLocationPicker();
                }
            }
        }

        private void RemoveAllElement()
        {
            var count = _layout.Children.Count - 1;
            for (int i = count; i >= 0; i--)
            {
                _layout.Children.RemoveAt(i);
            }
        }

        private void AddActivityIndicator()
        {
            var img = new Image
            {
                Aspect = Aspect.AspectFit,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
            img.Source = "globe.png";

            _layout.Children.Add(img);

            _layout.Children.Add(new ActivityIndicator
                {
                    IsRunning = true,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                });
            
            _layout.Children.Add(new Label
                {
                    Text = c_loadingMessage,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                });
        }

        private void AddTryAgainButton()
        {
            var label = new Label
            {
                Text = c_cannotReachServerErrorMessage,
            };
            
            var tryAgainButton = new Button
            {
                Text = "Try Again",
                BorderColor = Color.Black,
                BorderWidth = 1
            };
            tryAgainButton.Clicked += (object sender, EventArgs e) =>
            {
                RemoveAllElement();
                AddActivityIndicator();
                LoadInfo();
            };

            _layout.Children.Add(label);
            _layout.Children.Add(tryAgainButton);
        }

        private void AddLocationPicker()
        {
            var label = new Label
            {
                Text = c_cannotDetectGeolocationMessage,
            };
            _layout.Children.Add(label);

            var picker = new Picker
            {
                Title = "Province",
                BindingContext = LoadingLocationViewModel.UserSelectedLocation,
            };
            _layout.Children.Add(picker);

            foreach (var location in LoadingLocationViewModel.RaffleLocations)
            {
                picker.Items.Add(location.Name);
            }
            picker.SetBinding(Picker.SelectedIndexProperty, new Binding("Name", BindingMode.TwoWay, new PickerRaffleLocationNameToIndexConverter(), LoadingLocationViewModel.RaffleLocations));

            var button = new Button
            {
                Text = "Next",
            };
            _layout.Children.Add(button);
            button.Clicked += (sender, e) =>
            {
                MessagingCenter.Send<LoadingLocationPage>(this, "Success");
            };
        }
    }
}