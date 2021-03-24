using System;
using System.Globalization;
using System.Windows.Data;

namespace WPFVersion3D.Converters
{
    internal sealed class DoubleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Math.Round((double)value, 2);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return double.TryParse(value.ToString(), out double result) ? result : 0D;
        }
    }
}
