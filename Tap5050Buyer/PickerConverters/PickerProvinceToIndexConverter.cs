using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Tap5050Buyer
{
    /// <summary>
    /// Picker: string to int converter. Convert the selected index of the Picker to the string of the item and vice versa.
    /// Converter parameters: List<string> contains the items in the picker.
    /// </summary>
    // It should be better to make these converters "generic" using interface like IHasName for RaffleLocation, Country, Province...!!
    public class PickerProvinceToIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return 0;
            }
            var itemToFind = (string)value;
            var itemList = (List<Province>)parameter;
            return itemList.FindIndex(x => x.ProvinceName == itemToFind);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var index = (int)value;
            if (index < 0)
            {
                return null;
            }
            else
            {
                var itemList = (List<Province>)parameter;
                return itemList[index].ProvinceName;
            }
        }
    }
}

