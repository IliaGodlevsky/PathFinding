using GraphLib.Interface;

namespace GraphLib.Common.NullObjects
{
    public sealed class DefaultCost : IVertexCost
    {
        public int CurrentCost => default;
    }
}
