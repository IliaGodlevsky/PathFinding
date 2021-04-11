using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Media3D;

namespace WPFVersion3D.Converters
{
    internal sealed class InvertPointConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Point3D result = default;
            try
            {
                result = (Point3D)System.Convert.ChangeType(value, typeof(Point3D));
                var zeroPoint = new Point3D(0, 0, 0);
                return zeroPoint - result;
            }
            catch (Exception)
            {
                return result;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
    }
}
