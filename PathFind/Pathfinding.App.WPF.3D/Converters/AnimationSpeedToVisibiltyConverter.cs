using Pathfinding.App.WPF._3D.Model;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Pathfinding.App.WPF._3D.Converters
{
    internal sealed class AnimationSpeedToVisibiltyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is AnimationSpeeds.CustomAnimationSpeed
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
