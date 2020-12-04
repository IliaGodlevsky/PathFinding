using System;
using System.Globalization;
using System.Windows.Data;
using Wpf3dVersion.Enums;

namespace Wpf3dVersion.Converters
{
    internal class RotateDirectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == true)
                return RotateDirection.Forward;
            else
                return RotateDirection.Back;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((RotateDirection)value == RotateDirection.Forward)
                return true;
            else
                return false;
        }
    }
}
