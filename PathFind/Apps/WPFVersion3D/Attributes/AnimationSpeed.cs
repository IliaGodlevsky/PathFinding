namespace WPFVersion3D.Attributes
{
    internal sealed class AnimationSpeed : BaseAnimationSpeed
    {
        public override double Milliseconds { get; }

        public AnimationSpeed(double milliseconds)
        {
            Milliseconds = milliseconds;
        }
    }
}