using Common.Extensions;
using System;
using System.Globalization;
using System.Windows.Data;
using WPFVersion3D.Enums;

namespace WPFVersion3D.Converters
{
    internal sealed class AlgorithmStatusToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is AlgorithmStatuses status)
            {
                return status.GetDescriptionAttributeValueOrDefault();
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}