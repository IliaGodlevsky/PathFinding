using System;
using System.Globalization;
using System.Windows.Data;
using WPFVersion3D.Model;

namespace WPFVersion3D.Converters
{
    internal sealed class AnimationSpeedToDataContextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is AnimationSpeedsCollection.CustomAnimationSpeed ? value : Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
