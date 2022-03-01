namespace WPFVersion3D.Attributes
{
    internal sealed class AnimationSpeed : BaseAnimationSpeed
    {
        public AnimationSpeed(double milliseconds)
        {
            Milliseconds = milliseconds;
        }

        public override double Milliseconds { get; }
    }
}