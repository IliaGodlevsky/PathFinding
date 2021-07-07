using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using WPFVersion3D.Infrastructure.AnimationSpeed;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Infrastructure.Animators
{
    internal abstract class AnimatedAxisRotator : IAnimatedAxisRotator
    {
        protected AnimatedAxisRotator(AxisAngleRotation3D axis, IAnimationSpeed speed)
        {
            this.axis = axis;
            this.speed = speed;
        }

        protected AnimatedAxisRotator(AxisAngleRotation3D axis)
             : this(axis, new SlowSpeed())
        {

        }

        public void RotateAxis()
        {
            axis.BeginAnimation(AxisAngleRotation3D.AngleProperty, CreateAnimation());
        }

        protected abstract AnimationTimeline CreateAnimation();

        protected abstract Duration CalculateAnimationDuration();

        protected readonly AxisAngleRotation3D axis;
        protected readonly IAnimationSpeed speed;
    }
}
