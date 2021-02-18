using Common;
using System;
using System.Globalization;
using System.Windows.Data;

namespace WPFVersion3D.Converters
{
    internal class StringToRangedIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int result = default;

            if (IsValidParametres(value, parameter))
            {
                var range = parameter as ValueRange;
                result = System.Convert.ToInt32(value);
                result = range.ReturnInRange(result);
            }

            return result;
        }

        private bool IsValidParametres(object value, object parametre)
        {
            return int.TryParse(value.ToString(), out _) && parametre is ValueRange;
        }
    }
}
