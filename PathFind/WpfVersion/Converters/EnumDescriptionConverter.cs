using GraphLibrary.Enums.AlgorithmEnum;
using GraphLibrary.Extensions;
using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfVersion.Converters
{
    public class AlgorithmDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Algorithms)value).GetDescription();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Empty;
        }
    }
}
