using Pathfinding.App.WPF._3D.Interface;
using Pathfinding.App.WPF._3D.Model.RotatorFactories;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace Pathfinding.App.WPF._3D.Converters
{
    internal sealed class AnimatedAxisRotatorConverter : IValueConverter
    {
        private IReadOnlyDictionary<object, IAnimatedAxisRotatorFactory> Rotators { get; }

        public AnimatedAxisRotatorConverter()
        {
            Rotators = new Dictionary<object, IAnimatedAxisRotatorFactory>()
            {
                { true, new ForwardAnimatedAxisRotatorFactory() },
                { false, new BackwardAnimatedAxisRotatorFactory() },
            }.ToReadOnly();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Rotators.GetOrDefault(value, NullAnimatedAxisRotatorFactory.Interface);
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}