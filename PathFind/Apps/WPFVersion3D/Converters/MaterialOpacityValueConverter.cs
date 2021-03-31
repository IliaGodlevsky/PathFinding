using System;
using System.Globalization;
using System.Windows.Data;

namespace WPFVersion3D.Converters
{
    internal sealed class MaterialOpacityValueConverter : IValueConverter
    {
        private readonly IValueConverter doubleToStringConverter;

        public MaterialOpacityValueConverter()
        {
            doubleToStringConverter = new DoubleToStringConverter();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return doubleToStringConverter.Convert(value, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
    }
}
