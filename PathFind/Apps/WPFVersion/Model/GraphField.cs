using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using GraphLib.Realizations.Graphs;
using System.Collections.Generic;
using System.Windows.Controls;

using static WPFVersion.Constants;

namespace WPFVersion.Model
{
    internal sealed class GraphField : Canvas, IGraphField
    {
        public IReadOnlyCollection<IVertex> Vertices { get; }

        public GraphField(Graph2D graph)
        {
            graph.Vertices.ForEach(Add);
            Vertices = graph.Vertices;
        }

        public GraphField()
        {

        }

        private void Add(IVertex vertex)
        {
            if (vertex is Vertex wpfVertex && wpfVertex.Position is Coordinate2D coordinates)
            {
                base.Children.Add(wpfVertex);
                SetLeft(wpfVertex, (DistanceBetweenVertices + wpfVertex.Width) * coordinates.X);
                SetTop(wpfVertex, (DistanceBetweenVertices + wpfVertex.Height) * coordinates.Y);
            }
        }
    }
}
