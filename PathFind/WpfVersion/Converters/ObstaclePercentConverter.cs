using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfVersion.Converters
{
    public class ObstaclePercentConverter : IValueConverter
    {
        public const int MAX_SLIDER_VALUE = 100;
        public const int MIN_SLIDER_VALUE = 0;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Math.Round((double)value, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (double.TryParse(value.ToString(), out double result))
            {
                if (result >= MIN_SLIDER_VALUE && result <= MAX_SLIDER_VALUE)
                    return result;
                else if (result > MAX_SLIDER_VALUE)
                    return (double)MAX_SLIDER_VALUE;
                else
                    return (double)MIN_SLIDER_VALUE;
            }
            else
                return 0D;
        }
    }
}
