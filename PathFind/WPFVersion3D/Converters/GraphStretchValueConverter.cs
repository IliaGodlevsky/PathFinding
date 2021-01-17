using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using WPFVersion3D.Enums;

namespace WPFVersion3D.Converters
{
    internal class GraphStretchValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (IsValidParametres(values))
            {
                var axis = (Axis)values.First(value => value is Axis);
                var newValue = (double)values.First(value => value is double);
                var offset = (double[])values.First(value => value.GetType().IsArray);

                return new Tuple<Axis, double, double[]>(axis, newValue, offset);
            }

            throw new ArgumentException();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[] { };
        }

        private bool IsValidParametres(object[]values)
        {
            return values?.Count(value => value is Axis) == 1
                && values?.Count(value => value is double) == 1
                && values?.Count(value => value.GetType().IsArray) == 1;
        }
    }
}
