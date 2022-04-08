using System;
using System.Globalization;
using System.Windows.Data;
using WPFVersion3D.Model.RotatorFactories;

namespace WPFVersion3D.Converters
{
    internal sealed class AnimatedAxisRotatorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case true: return new ForwardAnimatedAxisRotatorFactory();
                case false: return new BackwardAnimatedAxisRotatorFactory();
                default: return new NullAnimatedAxisRotatorFactory();
            }
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}