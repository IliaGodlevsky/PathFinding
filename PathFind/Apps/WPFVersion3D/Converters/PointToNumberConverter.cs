using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Media3D;

namespace WPFVersion3D.Converters
{
    internal sealed class PointToNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Point3D result = default;
            try
            {
                result = (Point3D)System.Convert.ChangeType(value, typeof(Point3D));
                return result.Z;
            }
            catch (Exception)
            {
                return result.Z;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double result = default;
            try
            {
                result = System.Convert.ToDouble(value);
                return new Point3D(result, result, result);
            }
            catch (Exception)
            {
                return new Point3D(result, result, result);
            }
        }
    }
}
