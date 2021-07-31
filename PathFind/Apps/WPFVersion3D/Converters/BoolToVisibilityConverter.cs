using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFVersion3D.Converters
{
    internal sealed class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isChecked)
            {
                return isChecked ? Visibility.Visible : Visibility.Collapsed;
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibility)
            {
                return visibility == Visibility.Visible ? true : false;
            }

            return Binding.DoNothing;
        }
    }
}
