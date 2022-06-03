using GraphLib.Interfaces;
using NullObject.Attributes;
using SingletonLib;

namespace GraphLib.Realizations.SmoothLevel
{
    [Null]
    public sealed class NullSmoothLevel : Singleton<NullSmoothLevel, ISmoothLevel>, ISmoothLevel
    {
        public int Level => 0;

        private NullSmoothLevel()
        {

        }
    }
}
