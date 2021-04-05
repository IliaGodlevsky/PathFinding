using System;
using System.Globalization;
using System.Windows.Data;

namespace WPFVersion3D.Converters
{
    internal sealed class DoubleToStringConverter : IValueConverter
    {
        public int Precision { get; set; } = 2;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double result = default;
            try
            {
                value = value?.ToString().Replace('.', ',');
                result = System.Convert.ToDouble(value);
                result = Math.Round(result, Precision);
                return result;

            }
            catch (Exception)
            {
                return result;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
    }
}
