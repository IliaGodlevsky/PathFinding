using NullObject.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media.Media3D;
using WPFVersion3D.Infrastructure.Animators;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Converters
{
    internal sealed class AnimatedAxisRotatorMultiConverter : IMultiValueConverter
    {
        public AnimatedAxisRotatorMultiConverter()
        {
            animatorFactories = new Dictionary<bool, Func<AxisAngleRotation3D, IAnimationSpeed, IAnimator>>()
            {
                { true,  CreateForwardAxisRotator },
                { false, CreateBackwardAxisRotator }
            };
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var animationSpeed = values.OfType<IAnimationSpeed>().FirstOrDefault();
            var type = values.OfType<bool>().FirstOrDefault();
            if (IsValidParametres(parameter, animationSpeed, type))
            {
                var axisAngleRotation = parameter as AxisAngleRotation3D;
                return animatorFactories[type](axisAngleRotation, animationSpeed);
            }

            return new NullAnimatedAxisRotator();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[] { Binding.DoNothing, Binding.DoNothing };
        }

        private bool IsValidParametres(object paramter, params object[] values)
        {
            return values.All(value => !value.IsNullObject())
                && paramter is AxisAngleRotation3D;
        }

        private IAnimator CreateForwardAxisRotator(AxisAngleRotation3D axis, IAnimationSpeed speed)
        {
            return new ForwardAnimatedAxisRotator(axis, speed);
        }

        private IAnimator CreateBackwardAxisRotator(AxisAngleRotation3D axis, IAnimationSpeed speed)
        {
            return new BackwardAnimatedAxisRotator(axis, speed);
        }

        private readonly Dictionary<bool, Func<AxisAngleRotation3D, IAnimationSpeed, IAnimator>> animatorFactories;
    }
}
