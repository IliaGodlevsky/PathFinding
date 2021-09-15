using WPFVersion3D.Interface;

namespace WPFVersion3D.Attributes
{
    internal sealed class Speed : BaseSpeed, IAnimationSpeed
    {
        public Speed(double milliseconds)
        {
            Milliseconds = milliseconds;
        }

        public override double Milliseconds { get; }
    }
}