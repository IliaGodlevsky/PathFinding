using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using WPFVersion3D.Enums;
using WPFVersion3D.Model.Interface;

namespace WPFVersion3D.Model
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

            DurationCalculateFunctions = new Dictionary<RotationDirection, Func<double>>()
            {
                { RotationDirection.Backward, CalculateBackwardAnimationDuration },
                { RotationDirection.Forward, CalculateForwardAnimationDuration }
            };

            AnimationCreationFunctions = new Dictionary<RotationDirection, Func<DoubleAnimation>>()
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
            var duration = DurationCalculateFunctions[direction]();
            return new Duration(TimeSpan.FromMilliseconds(duration));
        }

        private double CalculateForwardAnimationDuration() => InitialDuration * (AngleAmplitude - axis.Angle) / AngleAmplitude;

        private double CalculateBackwardAnimationDuration() => InitialDuration * axis.Angle / AngleAmplitude;

        private double AngleAmplitude => EndAngle - StartAngle;

        private double InitialDuration => 3000;

        private readonly AxisAngleRotation3D axis;
        private readonly RotationDirection direction;

        private Dictionary<RotationDirection, Func<double>> DurationCalculateFunctions { get; set; }
        private Dictionary<RotationDirection, Func<DoubleAnimation>> AnimationCreationFunctions { get; set; }
    }
}
