using NullObject.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media.Media3D;
using WPFVersion3D.Infrastructure.Animators;
using WPFVersion3D.Interface;
using WPFVersion3D.RotatorsFactories;

namespace WPFVersion3D.Converters
{
    internal sealed class AnimatedAxisRotatorMultiConverter : IMultiValueConverter
    {
        private readonly Dictionary<bool?, IAnimatedAxisRotatorFactory> animatorFactories;

        public AnimatedAxisRotatorMultiConverter()
        {
            animatorFactories = new Dictionary<bool?, IAnimatedAxisRotatorFactory>()
            {
                { true,  new ForwardAnimatedAxisRotatorFactory() },
                { false, new BackwardAnimatedAxisRotatorFactory() }
            };
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var animationSpeed = values?.OfType<IAnimationSpeed>().FirstOrDefault();
            var axisAngleRotation = parameter as AxisAngleRotation3D;
            bool? type = values?.OfType<bool?>().FirstOrDefault();

            return AreValidParametres(animationSpeed, type, axisAngleRotation)
                ? animatorFactories[type].CreateRotator(axisAngleRotation, animationSpeed)
                : NullAnimatedAxisRotator.Instance;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[] { Binding.DoNothing, Binding.DoNothing };
        }

        private bool AreValidParametres(params object[] values)
        {
            return !values.ContainsNulls();
        }
    }
}