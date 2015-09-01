using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Tap5050Buyer
{
    public partial class VersionPage : ContentPage
    {
        public VersionPage()
        {
            InitializeComponent();
            Title = "Version0.7";

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children =
                {new Label
                    {
                        Text = "Version 0.7",
                    }
                },
            };
        }
    }
}

