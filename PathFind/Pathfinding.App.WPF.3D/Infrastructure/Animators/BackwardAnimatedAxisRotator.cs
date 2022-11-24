using Pathfinding.App.WPF._3D.Interface;
using Shared.Extensions;
using Shared.Primitives.Extensions;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

using static Pathfinding.App.WPF._3D.Constants;

namespace Pathfinding.App.WPF._3D.Infrastructure.Animators
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
            var duration = speed.Time.Multiply(angleRotation.Angle / AngleValueRange.Amplitude());
            return new Duration(duration);
        }
    }
}
