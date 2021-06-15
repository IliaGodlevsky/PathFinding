using System;
using System.Globalization;
using System.Windows.Data;

namespace WPFVersion.Converters
{
    internal sealed class PrecisedDoubleToStringConverter : IValueConverter
    {
        public int Precision { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                value = value?.ToString().Replace('.', ',');
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
            if (IsValidParametres(value))
            {
                value = value?.ToString().Replace('.', ',');
                return System.Convert.ToDouble(value);
            }

            return Binding.DoNothing;
        }

        private bool IsValidParametres(object value)
        {
            return double.TryParse(value?.ToString(), out _);
        }
    }
}
