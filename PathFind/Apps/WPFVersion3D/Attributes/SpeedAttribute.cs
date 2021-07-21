namespace WPFVersion3D.Attributes
{
    internal sealed class Speed : BaseSpeed
    {
        public Speed(double milliseconds)
        {
            Milliseconds = milliseconds;
        }

        public override double Milliseconds { get; }
    }
}