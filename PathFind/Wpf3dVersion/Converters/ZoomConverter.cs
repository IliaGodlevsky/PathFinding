using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Media3D;

namespace Wpf3dVersion.Converters
{
    public class ZoomConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Point3D)value).Z;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double point = (double)value;
            return new Point3D(point, point, point);
        }
    }
}
