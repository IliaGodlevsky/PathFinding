using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using System.Collections.Generic;
using System.Windows.Controls;

using static WPFVersion.Constants;

namespace WPFVersion.Model
{
    internal sealed class GraphField : Canvas, IGraphField
    {
        public IReadOnlyCollection<IVertex> Vertices { get; }

        public GraphField(IGraph graph)
        {
            Vertices = graph;
            Vertices.ForEach(vertex => Locate((Vertex)vertex));
        }

        public GraphField()
        {

        }

        private void Locate(Vertex vertex)
        {
            var position = (Coordinate2D)vertex.Position;
            Children.Add(vertex);
            SetLeft(vertex, (DistanceBetweenVertices + vertex.Width) * position.X);
            SetTop(vertex, (DistanceBetweenVertices + vertex.Height) * position.Y);
        }
    }
}
