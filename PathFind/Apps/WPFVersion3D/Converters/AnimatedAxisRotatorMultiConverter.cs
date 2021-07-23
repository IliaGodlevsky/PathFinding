using NullObject.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media.Media3D;
using WPFVersion3D.Infrastructure.Animators;
using WPFVersion3D.Interface;

using AnimatorFatory = System.Func<System.Windows.Media.Media3D.AxisAngleRotation3D,
    WPFVersion3D.Interface.IAnimationSpeed, WPFVersion3D.Interface.IAnimatedAxisRotator>;

namespace WPFVersion3D.Converters
{
    internal sealed class AnimatedAxisRotatorMultiConverter : IMultiValueConverter
    {
        public AnimatedAxisRotatorMultiConverter()
        {
            animatorFactories = new Dictionary<bool?, AnimatorFatory>()
            {
                { true,  CreateForwardAxisRotator },
                { false, CreateBackwardAxisRotator }
            };
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var animationSpeed = values?.OfType<IAnimationSpeed>().FirstOrDefault();
            var axisAngleRotation = parameter as AxisAngleRotation3D;
            bool? type = values?.OfType<bool?>().FirstOrDefault();

            return IsValidParametres(animationSpeed, type, axisAngleRotation)
                ? animatorFactories[type](axisAngleRotation, animationSpeed)
                : new NullAnimatedAxisRotator();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[] { Binding.DoNothing, Binding.DoNothing };
        }

        private bool IsValidParametres(params object[] values)
        {
            return !values.ContainsNulls();
        }

        private IAnimatedAxisRotator CreateForwardAxisRotator(AxisAngleRotation3D axis, IAnimationSpeed speed)
        {
            return new ForwardAnimatedAxisRotator(axis, speed);
        }

        private IAnimatedAxisRotator CreateBackwardAxisRotator(AxisAngleRotation3D axis, IAnimationSpeed speed)
        {
            return new BackwardAnimatedAxisRotator(axis, speed);
        }

        private readonly Dictionary<bool?, AnimatorFatory> animatorFactories;
    }
}
