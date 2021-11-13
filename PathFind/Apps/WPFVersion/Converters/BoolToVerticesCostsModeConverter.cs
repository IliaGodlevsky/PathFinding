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
            switch (value)
            {
                case true: return new WeightedVerticesCostsMode();
                case false: return new UnweightedVerticesCostsMode();
                default: return Binding.DoNothing;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
