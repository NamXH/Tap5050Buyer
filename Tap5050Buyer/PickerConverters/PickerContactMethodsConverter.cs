using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Tap5050Buyer
{
    public class PickerContactMethodsConverter : IValueConverter
    {
        private List<string> _methods = new List<string>() {"NOCONTACT", "SMS", "EMAIL"};

        // From contact methods to index
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var method = (string)value;
            return _methods.FindIndex(x => x == method);
        }

        // From index to contact methods
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var index = (int)value;
            if (index == -1)
            {
                return String.Empty; // This condition is unreachable for some reason!! Bug!!
            }
            else
            {
                return _methods[index];
            }
        }
    }
}

