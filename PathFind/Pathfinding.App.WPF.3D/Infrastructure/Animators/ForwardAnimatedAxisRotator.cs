using Pathfinding.App.WPF._3D.Interface;
using Shared.Extensions;
using Shared.Primitives.Extensions;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

using static Pathfinding.App.WPF._3D.Constants;

namespace Pathfinding.App.WPF._3D.Infrastructure.Animators
{
    internal sealed class ForwardAnimatedAxisRotator : AnimatedAxisRotator
    {
        public ForwardAnimatedAxisRotator(IAnimationSpeed speed)
            : base(speed)
        {

        }

        protected override AnimationTimeline CreateAnimation(AxisAngleRotation3D angleRotation)
        {
            var duration = CalculateAnimationDuration(angleRotation);
            return new DoubleAnimation(angleRotation.Angle, AngleValueRange.UpperValueOfRange, duration, FillBehavior.HoldEnd);
        }

        protected override Duration CalculateAnimationDuration(AxisAngleRotation3D angleRotation)
        {
            var angleAmplitude = AngleValueRange.Amplitude();
            var duration = speed.Time.Multiply((angleAmplitude - angleRotation.Angle) / angleAmplitude);
            return new Duration(duration);
        }
    }
}
