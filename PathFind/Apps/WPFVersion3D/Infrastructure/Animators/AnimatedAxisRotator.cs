using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Infrastructure.Animators
{
    internal abstract class AnimatedAxisRotator : IAnimatedAxisRotator
    {
        protected readonly IAnimationSpeed speed;

        protected AnimatedAxisRotator(IAnimationSpeed speed)
        {
            this.speed = speed;
        }

        public void RotateAxis(AxisAngleRotation3D angleRotation)
        {
            angleRotation.BeginAnimation(AxisAngleRotation3D.AngleProperty, CreateAnimation(angleRotation));
        }

        protected abstract AnimationTimeline CreateAnimation(AxisAngleRotation3D angleRotation);

        protected abstract Duration CalculateAnimationDuration(AxisAngleRotation3D angleRotation);
    }
}