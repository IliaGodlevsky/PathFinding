using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using WPFVersion3D.Enums;
using WPFVersion3D.Infrastructure.Animators.Interface;
using static WPFVersion3D.Constants;
using AnimationCreationFunctions =
    System.Collections.Generic.Dictionary
    <WPFVersion3D.Enums.RotationDirection, System.Func<System.Windows.Media.Animation.DoubleAnimation>>;
using DurationCalculationFunctions =
    System.Collections.Generic.Dictionary
    <WPFVersion3D.Enums.RotationDirection, System.Func<double>>;

namespace WPFVersion3D.Infrastructure.Animators
{
    /// <summary>
    /// A class for animated rotation 
    /// of <see cref="AxisAngleRotation3D"/>
    /// in selected <see cref="RotationDirection"/>
    /// </summary>
    internal class AnimatedAxisRotator : IAnimator
    {
        public static double StartAngle => 0;

        public static double EndAngle => 360;

        public AnimatedAxisRotator(AxisAngleRotation3D axis, RotationDirection direction)
        {
            this.axis = axis;
            this.direction = direction;

            DurationCalculationFunctions = new DurationCalculationFunctions()
            {
                { RotationDirection.Backward, CalculateBackwardAnimationDuration },
                { RotationDirection.Forward, CalculateForwardAnimationDuration }
            };

            AnimationCreationFunctions = new AnimationCreationFunctions()
            {
                { RotationDirection.Backward, () => CreateAnimation(axis.Angle, StartAngle, FillBehavior.Stop) },
                { RotationDirection.Forward, () => CreateAnimation(axis.Angle, EndAngle, FillBehavior.HoldEnd) },
            };
        }

        public void ApplyAnimation()
        {
            var animation = AnimationCreationFunctions[direction]();
            axis.BeginAnimation(AxisAngleRotation3D.AngleProperty, animation);
        }

        private DoubleAnimation CreateAnimation(double from,
            double to, FillBehavior fillBehavior)
        {
            var duration = CalculateAnimationDuration();
            return new DoubleAnimation(from, to, duration, fillBehavior);
        }

        private Duration CalculateAnimationDuration()
        {
            var duration = DurationCalculationFunctions[direction]();
            return new Duration(TimeSpan.FromMilliseconds(duration));
        }

        private double CalculateForwardAnimationDuration()
        {
            return InitialRotationAnimationDuration * (AngleAmplitude - axis.Angle) / AngleAmplitude;
        }

        private double CalculateBackwardAnimationDuration()
        {
            return InitialRotationAnimationDuration * axis.Angle / AngleAmplitude;
        }

        private static double AngleAmplitude => EndAngle - StartAngle;

        private DurationCalculationFunctions DurationCalculationFunctions { get; }
        private AnimationCreationFunctions AnimationCreationFunctions { get; }

        private readonly AxisAngleRotation3D axis;
        private readonly RotationDirection direction;
    }
}
