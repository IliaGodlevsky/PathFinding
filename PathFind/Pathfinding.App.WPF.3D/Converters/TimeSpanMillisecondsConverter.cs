using System;
using System.Globalization;
using System.Windows.Data;

namespace Pathfinding.App.WPF._3D.Converters
{
    internal sealed class TimeSpanMillisecondsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimeSpan span)
            {
                return span.TotalMilliseconds;
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                double timeMilliseconds = System.Convert.ToDouble(value);
                return TimeSpan.FromMilliseconds(timeMilliseconds);
            }
            catch (Exception)
            {
                return Binding.DoNothing;
            }
        }
    }
}
