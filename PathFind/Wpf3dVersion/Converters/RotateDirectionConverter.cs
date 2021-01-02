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
                return RotationDirection.Forward;
            else
                return RotationDirection.Backward;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((RotationDirection)value == RotationDirection.Forward)
                return true;
            else
                return false;
        }
    }
}
