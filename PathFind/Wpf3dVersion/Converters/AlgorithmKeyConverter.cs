using System;
using System.Globalization;
using System.Windows.Data;

namespace Wpf3dVersion.Converters
{
    internal class AlgorithmKeyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Empty;
        }
    }
}
