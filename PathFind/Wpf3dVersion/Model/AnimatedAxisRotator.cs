using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using Wpf3dVersion.Enums;
using Wpf3dVersion.Model.Interface;

namespace Wpf3dVersion.Model
{
    internal class AnimatedAxisRotator : IAnimator
    {
        public static double StartAngle => 0;

        public static double EndAngle => 360;

        public AnimatedAxisRotator(AxisAngleRotation3D axis, RotationDirection direction)
        {
            this.axis = axis;
            this.direction = direction;
        }

        public void ApplyAnimation()
        {
            DoubleAnimation animation = null;

            switch (direction)
            {
                case RotationDirection.Forward: 
                    animation = CreateAnimation(axis.Angle, EndAngle, FillBehavior.HoldEnd);
                    break;
                case RotationDirection.Backward:
                    animation = CreateAnimation(axis.Angle, StartAngle, FillBehavior.Stop); 
                    break;
            }

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
            var duration = default(double);

            switch (direction)
            {
                case RotationDirection.Forward:
                    duration = CalculateForwardAnimationDuration();
                    break;
                case RotationDirection.Backward:
                    duration = CalculateBackwardAnimationDuration();
                    break;
            }

            return new Duration(TimeSpan.FromMilliseconds(duration));
        }

        private double CalculateForwardAnimationDuration()
        {
            return InitialDuration * (AngleAmplitude - axis.Angle) / AngleAmplitude;
        }

        private double CalculateBackwardAnimationDuration()
        {
            return InitialDuration * axis.Angle / AngleAmplitude;
        }

        private double AngleAmplitude => EndAngle - StartAngle;

        private double InitialDuration => 3000;

        private readonly AxisAngleRotation3D axis;
        private readonly RotationDirection direction;
    }
}
