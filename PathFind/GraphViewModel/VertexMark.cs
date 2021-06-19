using Algorithm.Infrastructure.EventArguments;
using GraphLib.Interfaces;
using System;

namespace GraphViewModel
{
    public sealed class VertexMark
    {
        public void OnVertexVisited(object sender, EventArgs e)
        {
            if (e is AlgorithmEventArgs args)
            {
                if (!args.IsEndPoint && args.Vertex is IMarkable vertex)
                {
                    vertex.MarkAsVisited();
                }
            }
        }

        public void OnVertexEnqueued(object sender, EventArgs e)
        {
            if (e is AlgorithmEventArgs args)
            {
                if (!args.IsEndPoint && args.Vertex is IMarkable vertex)
                {
                    vertex.MarkAsEnqueued();
                }
            }
        }
    }
}
