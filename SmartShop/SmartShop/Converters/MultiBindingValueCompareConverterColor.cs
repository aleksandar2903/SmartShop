using System;
using System.Globalization;
using Xamarin.Forms;

namespace SmartShop.Converters
{
    internal class MultiBindingValueCompareConverterColor : IMultiValueConverter
    {
            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                if (values[0] is int id1 && values[1] is int id2 && id1 == id2)
                {
                    return "Black";
                }

                return "#ededed";
            }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
}
