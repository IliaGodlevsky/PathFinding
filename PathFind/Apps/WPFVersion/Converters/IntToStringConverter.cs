using System;
using System.Globalization;
using System.Windows.Data;

namespace WPFVersion.Converters
{
    internal sealed class IntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertBack(value, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return IsValidParametres(value) ? System.Convert.ToInt32(value) : Binding.DoNothing;
        }

        private bool IsValidParametres(object value)
        {
            return int.TryParse(value?.ToString(), out _);
        }
    }
}
