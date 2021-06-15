using Common.Extensions;
using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using WPFVersion3D.Infrastructure.Animators.Interface;

using static WPFVersion3D.Constants;

namespace WPFVersion3D.Infrastructure.Animators
{
    internal sealed class BackwardAnimatedAxisRotator : BaseAnimatedAxisRotator, IAnimator
    {
        public BackwardAnimatedAxisRotator(AxisAngleRotation3D axis) : base(axis)
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
            var duration = InitialRotationAnimationDuration * axis.Angle / AngleValueRange.Amplitude();
            return new Duration(TimeSpan.FromMilliseconds(duration));
        }
    }
}
