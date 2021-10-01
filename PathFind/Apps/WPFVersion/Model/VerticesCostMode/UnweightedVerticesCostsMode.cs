using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace WPFVersion.Model.VerticesCostMode
{
    internal sealed class UnweightedVerticesCostsMode : IVerticesCostsMode
    {
        public void Apply(IGraph graph)
        {
            graph.ToUnweighted();
        }
    }
}
