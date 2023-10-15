using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;

namespace Pathfinding.App.WPF._3D.Model
{
    internal sealed class GraphField3DFactory : IGraphFieldFactory<Vertex3D, GraphField3D>
    {
        public GraphField3D CreateGraphField(IGraph<Vertex3D> graph)
        {
            return new(graph);
        }
    }
}
