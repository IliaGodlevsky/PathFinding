using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace WPFVersion.Model
{
    internal sealed class GraphField : Canvas, IGraphField
    {
        public IReadOnlyCollection<IVertex> Vertices => Children.OfType<IVertex>().ToArray();

        public GraphField()
        {
            distanceBetweenVertices = Constants.DistanceBetweenVertices;
        }

        public void Add(IVertex vertex)
        {
            if (vertex is Vertex wpfVertex && wpfVertex.Position is Coordinate2D coordinates)
            {
                Children.Add(wpfVertex);
                SetLeft(wpfVertex, (distanceBetweenVertices + wpfVertex.Width) * coordinates.X);
                SetTop(wpfVertex, (distanceBetweenVertices + wpfVertex.Height) * coordinates.Y);
            }
        }

        public void Clear()
        {
            Children.Clear();
        }

        private readonly int distanceBetweenVertices;
    }
}
