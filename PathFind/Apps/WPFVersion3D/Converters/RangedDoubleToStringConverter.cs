using Common;
using System;
using System.Globalization;
using System.Windows.Data;

namespace WPFVersion3D.Converters
{
    internal class RangedDoubleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Math.Round((double)value, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double result = default;

            if (IsValidParametres(value, parameter))
            {
                var range = parameter as ValueRange;
                var val = System.Convert.ToInt32(value);
                result = range.ReturnInRange(val);
            }

            return result;
        }

        private bool IsValidParametres(object value, object parametre)
        {
            return double.TryParse(value.ToString(), out _) && parametre is ValueRange;
        }
    }
}
