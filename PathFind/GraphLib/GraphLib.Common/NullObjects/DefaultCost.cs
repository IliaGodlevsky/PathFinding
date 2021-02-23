using Common.Attributes;
using GraphLib.Interface;

namespace GraphLib.Common.NullObjects
{
    [Default]
    public sealed class DefaultCost : IVertexCost
    {
        public int CurrentCost => default;
    }
}
