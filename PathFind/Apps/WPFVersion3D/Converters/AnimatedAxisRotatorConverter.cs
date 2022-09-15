using Common.Extensions.EnumerableExtensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;
using WPFVersion3D.Interface;
using WPFVersion3D.Model.RotatorFactories;

namespace WPFVersion3D.Converters
{
    internal sealed class AnimatedAxisRotatorConverter : IValueConverter
    {
        private IReadOnlyDictionary<object, IAnimatedAxisRotatorFactory> Rotators { get; }

        public AnimatedAxisRotatorConverter()
        {
            var rotators = new Dictionary<object, IAnimatedAxisRotatorFactory>()
            {
                { true, new ForwardAnimatedAxisRotatorFactory() },
                { false, new BackwardAnimatedAxisRotatorFactory() },
            };
            Rotators = new ReadOnlyDictionary<object, IAnimatedAxisRotatorFactory>(rotators);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Rotators.GetOrDefault(value, () => NullAnimatedAxisRotatorFactory.Interface);
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}