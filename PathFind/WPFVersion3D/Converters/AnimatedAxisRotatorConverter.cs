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
            if (IsValidParametres(value, parameter))
            {
                var axisAngleRotation = parameter as AxisAngleRotation3D;
                var direction = ((bool)value) ? RotationDirection.Forward : RotationDirection.Backward;
                return new AnimatedAxisRotator(axisAngleRotation, direction);
            }

            throw new ArgumentException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new object();
        }

        private bool IsValidParametres(object value, object parametres)
        {
            return (value is bool) && (parametres as AxisAngleRotation3D != null);
        }
    }
}
