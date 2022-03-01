using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;

namespace ConsoleVersion.Model
{
    internal sealed class GraphFieldFactory : IGraphFieldFactory
    {
        public IGraphField CreateGraphField(IGraph graph)
        {
            return new GraphField((Graph2D)graph);
        }
    }
}
