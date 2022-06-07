using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using WPFVersion.Model;

namespace WPFVersion.Converters
{
    internal sealed class SmoothLevelToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is WpfSmoothLevels.CustomSmoothLevel ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
