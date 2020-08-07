using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace WpfVersion.Converters
{
    public class ObstaclePercentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Math.Round((double)value, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var range = (int[])parameter;
            if (value != null && range != null && (range?.Count()) == 2
                && double.TryParse(value.ToString(), out double result))
            {
                if (result >= range.First() && result <= range.Last())
                    return result;
                return result > range.Last() ? range.Last() : (double)0;
            }
            else
                return 0D;
        }
    }
}
