using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using WPFVersion3D.Enums;
using DurationCalculateFunction = System.Collections.Generic.Dictionary<WPFVersion3D.Enums.RotationDirection, System.Func<double>>;
using AnimationCreateFunction = System.Collections.Generic.Dictionary<WPFVersion3D.Enums.RotationDirection,
    System.Func<System.Windows.Media.Animation.DoubleAnimation>>;
using WPFVersion3D.Infrastructure.Animations.Interface;

namespace WPFVersion3D.Infrastructure.Animations
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

            DurationCalculateFunctions = new DurationCalculateFunction()
            {
                { RotationDirection.Backward, CalculateBackwardAnimationDuration },
                { RotationDirection.Forward, CalculateForwardAnimationDuration }
            };

            AnimationCreateFunctions = new AnimationCreateFunction()
            {
                { RotationDirection.Backward, () => CreateAnimation(axis.Angle, StartAngle, FillBehavior.Stop) },
                { RotationDirection.Forward, () => CreateAnimation(axis.Angle, EndAngle, FillBehavior.HoldEnd) },
            };
        }

        public void ApplyAnimation()
        {
            var animation = AnimationCreateFunctions[direction]();
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
            var duration = DurationCalculateFunctions[direction]();
            return new Duration(TimeSpan.FromMilliseconds(duration));
        }

        private double CalculateForwardAnimationDuration()
            => InitialDuration * (AngleAmplitude - axis.Angle) / AngleAmplitude;

        private double CalculateBackwardAnimationDuration()
            => InitialDuration * axis.Angle / AngleAmplitude;

        private double AngleAmplitude => EndAngle - StartAngle;

        private DurationCalculateFunction DurationCalculateFunctions { get; set; }
        private AnimationCreateFunction AnimationCreateFunctions { get; set; }

        private double InitialDuration => 3000;

        private readonly AxisAngleRotation3D axis;
        private readonly RotationDirection direction;


    }
}
