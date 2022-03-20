using NullObject.Attributes;
using SingletonLib;
using System.Windows.Media.Media3D;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Infrastructure.Animators
{
    [Null]
    internal sealed class NullAnimatedAxisRotator : Singleton<NullAnimatedAxisRotator, IAnimatedAxisRotator>, IAnimatedAxisRotator
    {
        private NullAnimatedAxisRotator()
        {

        }

        public void RotateAxis(AxisAngleRotation3D angleRotation)
        {

        }
    }
}