using Algorithm.Infrastructure.EventArguments;
using GraphLib.Interfaces;

namespace GraphViewModel
{
    public sealed class VertexMark
    {
        public void OnVertexVisited(object sender, AlgorithmEventArgs e)
        {
            if (!e.IsEndPoint && e.Vertex is IMarkable vertex)
            {
                vertex.MarkAsVisited();
            }
        }

        public void OnVertexEnqueued(object sender, AlgorithmEventArgs e)
        {
            if (!e.IsEndPoint && e.Vertex is IMarkable vertex)
            {
                vertex.MarkAsEnqueued();
            }
        }
    }
}
