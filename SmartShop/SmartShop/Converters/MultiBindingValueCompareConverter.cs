﻿using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace SmartShop.Converters
{
    internal class MultiBindingValueCompareConverter : IMultiValueConverter
    {
            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                if (values[0] is int selectedValue && values[1] is Dictionary<int, int> selectedValues && selectedValues.ContainsKey(selectedValue))
                {
                    return 1;
                }

            return 0;
        }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
}
