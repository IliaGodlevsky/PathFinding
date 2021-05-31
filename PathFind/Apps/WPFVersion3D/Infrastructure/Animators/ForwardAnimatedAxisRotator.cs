using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using WPFVersion3D.Infrastructure.Animators.Interface;

using static WPFVersion3D.Constants;

namespace WPFVersion3D.Infrastructure.Animators
{
    internal sealed class ForwardAnimatedAxisRotator : IAnimator
    {
        public ForwardAnimatedAxisRotator(AxisAngleRotation3D axis)
        {
            this.axis = axis;
        }

        public void ApplyAnimation()
        {
            axis.BeginAnimation(AxisAngleRotation3D.AngleProperty, CreateAnimation());
        }

        private DoubleAnimation CreateAnimation()
        {
            var duration = CalculateAnimationDuration(axis);
            return new DoubleAnimation(axis.Angle, EndAngle, duration, FillBehavior.HoldEnd);
        }

        private Duration CalculateAnimationDuration(AxisAngleRotation3D axis)
        {
            var duration = InitialRotationAnimationDuration * (AngleAmplitude - axis.Angle) / AngleAmplitude;
            return new Duration(TimeSpan.FromMilliseconds(duration));
        }

        private readonly AxisAngleRotation3D axis;
    }
}
