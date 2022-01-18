using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;

namespace ConsoleVersion.Model
{
    internal sealed class GraphFieldFactory : IGraphFieldFactory
    {
        /// <summary>
        /// Creates graph field from <paramref name="graph"/>
        /// </summary>
        /// <param name="graph"></param>
        /// <returns>Graph field</returns>
        public IGraphField CreateGraphField(IGraph graph)
        {
            return new GraphField((Graph2D)graph);
        }
    }
}
