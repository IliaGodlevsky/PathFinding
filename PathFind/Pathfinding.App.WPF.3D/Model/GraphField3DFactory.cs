using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.VisualizationLib.Core.Interface;

namespace Pathfinding.App.WPF._3D.Model
{
    internal sealed class GraphField3DFactory : IGraphFieldFactory<Graph3D<Vertex3D>, Vertex3D, GraphField3D>
    {
        public GraphField3D CreateGraphField(Graph3D<Vertex3D> graph)
        {
            return new(graph);
        }
    }
}
