using System;
using System.Globalization;
using System.Windows.Data;

namespace WPFVersion3D.Converters
{
    internal sealed class PrecisedDoubleToStringConverter : IValueConverter
    {
        public int Precision { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string separator = culture.NumberFormat.NumberDecimalSeparator;
                value = value?.ToString().Replace(".", separator);
                var result = System.Convert.ToDouble(value);
                result = Math.Round(result, Precision);
                return result;
            }
            catch (Exception)
            {
                return Binding.DoNothing;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
    }
}
