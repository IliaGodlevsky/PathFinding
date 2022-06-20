using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using ValueRange.Extensions;
using WPFVersion3D.Interface;

using static WPFVersion3D.Constants;

namespace WPFVersion3D.Infrastructure.Animators
{
    internal sealed class BackwardAnimatedAxisRotator : AnimatedAxisRotator
    {
        public BackwardAnimatedAxisRotator(IAnimationSpeed speed)
            : base(speed)
        {

        }

        protected override AnimationTimeline CreateAnimation(AxisAngleRotation3D angleRotation)
        {
            var duration = CalculateAnimationDuration(angleRotation);
            return new DoubleAnimation(angleRotation.Angle, AngleValueRange.LowerValueOfRange, duration, FillBehavior.Stop);
        }

        protected override Duration CalculateAnimationDuration(AxisAngleRotation3D angleRotation)
        {
            var duration = speed.Time.TotalMilliseconds * angleRotation.Angle / AngleValueRange.Amplitude();
            return new Duration(TimeSpan.FromMilliseconds(duration));
        }
    }
}
