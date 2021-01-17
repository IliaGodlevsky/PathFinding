using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Media3D;
using WPFVersion3D.Enums;
using WPFVersion3D.Model;

namespace WPFVersion3D.Converters
{
    internal class AnimatedAxisRotatorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var axisAngleRotation = parameter as AxisAngleRotation3D;
            var direction = (bool)value == true ? RotationDirection.Forward : RotationDirection.Backward;
            return new AnimatedAxisRotator(axisAngleRotation, direction);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
