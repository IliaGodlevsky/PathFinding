using Common.Extensions;
using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using WPFVersion3D.Interface;
using static WPFVersion3D.Constants;

namespace WPFVersion3D.Infrastructure.Animators
{
    internal sealed class BackwardAnimatedAxisRotator : BaseAnimatedAxisRotator, IAnimator
    {
        public BackwardAnimatedAxisRotator(AxisAngleRotation3D axis) : base(axis)
        {

        }

        public BackwardAnimatedAxisRotator(AxisAngleRotation3D axis, IAnimationSpeed speed) 
            : base(axis, speed)
        {
        }

        protected override AnimationTimeline CreateAnimation()
        {
            var duration = CalculateAnimationDuration();
            return new DoubleAnimation(axis.Angle, AngleValueRange.LowerValueOfRange,
                duration, FillBehavior.Stop);
        }

        protected override Duration CalculateAnimationDuration()
        {
            var duration = speed.Speed * axis.Angle / AngleValueRange.Amplitude();
            return new Duration(TimeSpan.FromMilliseconds(duration));
        }
    }
}
