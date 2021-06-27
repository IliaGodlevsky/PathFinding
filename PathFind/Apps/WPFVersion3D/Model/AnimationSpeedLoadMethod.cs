using AssembleClassesLib.Interface;
using System.Reflection;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Model
{
    internal sealed class AnimationSpeedLoadMethod : IAssembleLoadMethod
    {
        public Assembly Load(string assemblyPath)
        {
            return typeof(IAnimationSpeed).Assembly;
        }
    }
}
