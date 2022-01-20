using GraphLib.Interfaces;
using NullObject.Attributes;
using System;

namespace GraphLib.NullRealizations.NullObjects
{
    [Null]
    [Serializable]
    public sealed class NullCost : IVertexCost
    {
        public static IVertexCost Instance => instance.Value;

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

        private static readonly Lazy<IVertexCost> instance = new Lazy<IVertexCost>(() => new NullCost());
    }
}
