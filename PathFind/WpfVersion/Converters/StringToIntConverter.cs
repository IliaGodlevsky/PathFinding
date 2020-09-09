using GraphLibrary.ValueRanges;
using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfVersion.Converters
{
    internal class StringToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int result = 0;
            var range = parameter as ValueRange;
            if (range == null || value == null)
                return result;
            if (int.TryParse(value.ToString(), out result))
                if (!range.IsInBounds(result))
                    result = range.LowerRange;
            return result;
        }
    }
}
