using GraphLibrary.Common.Constants;
using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfVersion.Converters
{
    internal class ObstaclePercentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Math.Round((double)value, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double result = 0D;
            if (value == null)
                return result;
            if (double.TryParse(value.ToString(), out result))
            {
                var obstaclePercent = System.Convert.ToInt32(result);
                if (GraphParametresRange.IsInValidObstacleRange(obstaclePercent))
                    result = obstaclePercent;
                else
                    result = GraphParametresRange.UpperObstacleValue;
            }
            return result;
        }
    }
}
