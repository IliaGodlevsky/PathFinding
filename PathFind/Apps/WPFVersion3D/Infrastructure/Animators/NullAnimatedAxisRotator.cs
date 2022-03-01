using NullObject.Attributes;
using SingletonLib;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Infrastructure.Animators
{
    [Null]
    internal sealed class NullAnimatedAxisRotator : Singleton<NullAnimatedAxisRotator, IAnimatedAxisRotator>, IAnimatedAxisRotator
    {
        public void RotateAxis()
        {

        }

        private NullAnimatedAxisRotator()
        {

        }
    }
}
