using Common.Extensions.EnumerableExtensions;
using Common.ReadOnly;
using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using System.Collections.Generic;
using System.Windows.Controls;

using static WPFVersion.Constants;

namespace WPFVersion.Model
{
    internal sealed class GraphField : Canvas, IGraphField<Vertex>
    {
        public IReadOnlyCollection<Vertex> Vertices { get; }

        public GraphField(IReadOnlyCollection<Vertex> graph)
        {
            Vertices = graph;
            Vertices.ForEach(vertex => Locate(vertex));
        }

        public GraphField() : this(ReadOnlyList<Vertex>.Empty)
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
