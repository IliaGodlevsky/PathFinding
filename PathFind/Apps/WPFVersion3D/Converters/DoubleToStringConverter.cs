using System;
using System.Globalization;
using System.Windows.Data;

namespace WPFVersion3D.Converters
{
    internal sealed class DoubleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double result = default;
            try
            {
                result = System.Convert.ToDouble(value);
                result = Math.Round(result, digits: 2);
                return result;

            }
            catch (Exception)
            {
                return result;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return double.TryParse(value?.ToString(), out var result) ? result : default;
        }
    }
}
