using Common.Attributes;
using GraphLib.Interface;

namespace GraphLib.Common.NullObjects
{
    [Default]
    public sealed class DefaultCost : IVertexCost
    {
        public int CurrentCost => default;

        public override bool Equals(object obj)
        {
            return obj is DefaultCost;
        }

        public override int GetHashCode()
        {
            return CurrentCost.GetHashCode();
        }
    }
}
