using WPFVersion3D.Interface;

namespace WPFVersion3D.Attributes
{
    internal sealed class AnimationSpeed : BaseAnimationSpeed, IAnimationSpeed
    {
        public AnimationSpeed(double milliseconds)
        {
            Milliseconds = milliseconds;
        }

        public override double Milliseconds { get; }
    }
}