using System;
using System.Globalization;
using System.Windows.Data;

namespace Wpf3dVersion.Converters
{
    internal class StringToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Math.Round((double)value, 2);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (double.TryParse(value.ToString(), out double result))
            {
                return result;
            }
            return 0;
        }
    }
}
