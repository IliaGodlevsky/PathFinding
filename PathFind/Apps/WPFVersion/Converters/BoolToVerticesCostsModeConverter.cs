using System;
using System.Globalization;
using System.Windows.Data;
using WPFVersion.Model.VerticesCostMode;

namespace WPFVersion.Converters
{
    internal sealed class BoolToVerticesCostsModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is true)
            {
                return new WeightedVerticesCostsMode();
            }
            else if (value is false)
            {
                return new UnweightedVerticesCostsMode();
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
