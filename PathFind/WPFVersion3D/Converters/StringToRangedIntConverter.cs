using Common.ValueRanges;
using System;
using System.Globalization;
using System.Windows.Data;

namespace WPFVersion3D.Converters
{
    internal class StringToRangedIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int result = 0;
            if (parameter is ValueRange range && value != null)
            {
                if (int.TryParse(value.ToString(), out result))
                {
                    if (!range.IsInRage(result))
                    {
                        result = range.LowerRange;
                    }
                }
            }
            return result;
        }
    }
}
