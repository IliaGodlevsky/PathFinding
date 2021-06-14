using GraphLib.Interfaces;
using NullObject.Attributes;
using System;

namespace GraphLib.NullRealizations.NullObjects
{
    [Null]
    [Serializable]
    public sealed class NullCost : IVertexCost
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

        public object Clone()
        {
            return new NullCost();
        }
    }
}
