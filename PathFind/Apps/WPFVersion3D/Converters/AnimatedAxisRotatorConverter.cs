using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Media3D;
using WPFVersion3D.Infrastructure.Animators;
using WPFVersion3D.Infrastructure.Animators.Interface;

namespace WPFVersion3D.Converters
{
    internal sealed class AnimatedAxisRotatorConverter : IValueConverter
    {
        public AnimatedAxisRotatorConverter()
        {
            animatorFactories = new Dictionary<bool, Func<AxisAngleRotation3D, IAnimator>>()
            {
                { true,  axisAngleRotation => new ForwardAnimatedAxisRotator(axisAngleRotation) },
                { false, axisAngleRotation => new BackwardAnimatedAxisRotator(axisAngleRotation) }
            };
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (IsValidParametres(value, parameter))
            {
                var axisAngleRotation = parameter as AxisAngleRotation3D;
                return animatorFactories[(bool)value](axisAngleRotation);
            }

            return new NullAnimatedAxisRotator();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is IAnimator animator 
                && animator is ForwardAnimatedAxisRotator;
        }

        private bool IsValidParametres(object value, object parametres)
        {
            return value is bool && parametres is AxisAngleRotation3D;
        }

        private readonly Dictionary<bool, Func<AxisAngleRotation3D, IAnimator>> animatorFactories;
    }
}
