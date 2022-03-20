using System;
using System.Globalization;
using System.Windows.Data;
using WPFVersion3D.Enums;

namespace WPFVersion3D.Converters
{
    internal sealed class AnimatedAxisRotatorConverter : IValueConverter
    {
        public object Convert(object values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values is bool isChecked)
            {
                switch (isChecked)
                {
                    case true: 
                        return AxisRotators.ForwardRotator;
                    case false: 
                        return AxisRotators.BackwardRotator;
                }
            }
            return AxisRotators.None;
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}