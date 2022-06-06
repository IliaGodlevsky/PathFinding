using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using WPFVersion3D.Model;

namespace WPFVersion3D.Converters
{
    internal sealed class AnimationSpeedToVisibiltyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is AnimationSpeedsCollection.CustomAnimationSpeed
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
