using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Tap5050Buyer
{
    public partial class ValidatePhonePage : ContentPage
    {
        public ValidatePhonePage()
        {
            InitializeComponent();
            _tableView.Intent = TableIntent.Menu;

            this.ToolbarItems.Add(new ToolbarItem("Done", null, () => {}));
        }
    }
}

