using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Tap5050Buyer
{
    public partial class TabsPage : TabbedPage 
    {
        public TabsPage()
        {
            InitializeComponent();
            this.SelectedItem = _raffleNavigationPage;
        }
    }
}