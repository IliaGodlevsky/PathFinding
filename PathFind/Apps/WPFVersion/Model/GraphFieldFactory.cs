using GraphLib.Interfaces;

namespace WPFVersion.Model
{
    internal sealed class GraphFieldFactory : IGraphFieldFactory
    {
        public IGraphField CreateGraphField(IGraph graph)
        {
            return new GraphField(graph);
        }
    }
}
