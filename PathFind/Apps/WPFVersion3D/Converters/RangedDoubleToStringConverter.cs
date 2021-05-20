using Common.ValueRanges;
using System;
using System.Globalization;
using System.Windows.Data;

namespace WPFVersion3D.Converters
{
    internal sealed class RangedDoubleToStringConverter : IValueConverter
    {
        private readonly DoubleToStringConverter doubleToStringConverter;

        public RangedDoubleToStringConverter()
        {
            doubleToStringConverter = new DoubleToStringConverter { Precision = 0 };
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return doubleToStringConverter.Convert(value, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double result = default;

            if (IsValidParametres(value, parameter))
            {
                var range = parameter as IValueRange;
                var val = System.Convert.ToInt32(value);
                result = range.ReturnInRange(val);
            }

            return result;
        }

        private bool IsValidParametres(object value, object parametre)
        {
            return double.TryParse(value.ToString(), out _) && parametre is IValueRange;
        }
    }
}
