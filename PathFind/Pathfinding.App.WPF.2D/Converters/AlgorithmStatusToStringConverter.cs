using System;
using System.Globalization;
using System.Windows.Data;

namespace Pathfinding.App.WPF._2D.Converters
{
    internal sealed class AlgorithmStatusToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value as string ?? string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}