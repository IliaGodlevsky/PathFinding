using GraphLib.Interfaces;
using NullObject.Attributes;
using SingletonLib;
using System;

namespace GraphLib.NullRealizations.NullObjects
{
    [Null]
    [Serializable]
    public sealed class NullCost : Singleton<NullCost>, IVertexCost
    {
        public int CurrentCost => default;

        public override bool Equals(object obj)
        {
            return obj is NullCost;
        }

        public override int GetHashCode()
        {
            return CurrentCost.GetHashCode();
        }

        public IVertexCost Clone()
        {
            return Instance;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        private NullCost()
        {

        }
    }
}
