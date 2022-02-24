using Common.Extensions;
using System;
using System.Globalization;
using System.Windows.Data;
using WPFVersion.Enums;

namespace WPFVersion.Converters
{
    internal sealed class AlgorithmStatusToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is AlgorithmStatus status)
            {
                return status.GetDescription();
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}