namespace WPFVersion3D.Interface
{
    internal interface IAnimatedAxisRotatorFactory
    {
        IAnimatedAxisRotator Create(IAnimationSpeed speed);
    }
}
