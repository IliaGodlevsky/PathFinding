using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace WPFVersion.Model.VerticesCostMode
{
    internal sealed class WeightedVerticesCostsMode : IVerticesCostsMode
    {
        public void Apply(IGraph graph)
        {
            graph.ToWeighted();
        }
    }
}
