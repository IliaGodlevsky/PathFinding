using GraphLibrary.Common.Constants;
using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfVersion.Converters
{
    internal class SliderValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Math.Round((double)value, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double result = 0D;
            if (!(parameter is ISliderValueRange sliderValueRange) || value == null)
                return result;
            if (double.TryParse(value.ToString(), out result))
            {
                var sliderValue = System.Convert.ToInt32(result);
                if (sliderValueRange.IsInBounds(sliderValue))
                    result = sliderValue;
                else
                    result = sliderValueRange.UpperRange;
            }
            return result;
        }
    }
}
