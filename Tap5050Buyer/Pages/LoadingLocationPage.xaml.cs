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
        internal const string c_cannotReachServerErrorMessage = "Cannot contact server. Please check your Internet connection and try again.";

        private LoadingLocationViewModel _viewModel;

        public LoadingLocationPage()
        {
            InitializeComponent();

            _viewModel = new LoadingLocationViewModel();

            AddActivityIndicator();

            LoadInfo();
        }

        public async void LoadInfo()
        {
            await _viewModel.GetCurrentLocationAndRaffleLocationList();

            if (LoadingLocationViewModel.RaffleLocations == null)
            {
                RemoveAllElement();
                AddTryAgainButton();
            }
            else
            {
                MessagingCenter.Send<LoadingLocationPage>(this, "Success");
            }
        }

        private void RemoveAllElement()
        {
            var count = layout.Children.Count - 1;
            for (int i = count; i >= 0; i--)
            {
                layout.Children.RemoveAt(i);
            }
        }

        private void AddActivityIndicator()
        {
            layout.Children.Add(new ActivityIndicator
                {
                    IsRunning = true
                });
            layout.Children.Add(new Label
                {
                    Text = c_loadingMessage
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

            layout.Children.Add(label);
            layout.Children.Add(tryAgainButton);
        }
    }
}