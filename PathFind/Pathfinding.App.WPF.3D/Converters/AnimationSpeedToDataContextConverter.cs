using Pathfinding.App.WPF._3D.Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Pathfinding.App.WPF._3D.Converters
{
    internal sealed class AnimationSpeedToDataContextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is AnimationSpeeds.CustomAnimationSpeed ? value : Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
