using System;
using System.Windows.Data;
using System.Windows.Media.Media3D;

namespace WPFVersion3D.Converters
{
    internal class InvertPointConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new Point3D(0, 0, 0) - (Point3D)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new Point3D(0, 0, 0) - (Point3D)value;
        }
    }
}
