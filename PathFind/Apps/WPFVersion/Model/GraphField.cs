using GraphLib.Interface;
using GraphLib.Realizations;
using System;
using System.Windows.Controls;

namespace WPFVersion.Model
{
    internal sealed class GraphField : Canvas, IGraphField
    {
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
            else
            {
                var message = "An error was occurred while adding vertex to a graph field\n";
                message += "Vertex must be 2D coordinate\n";
                throw new ArgumentException(message, nameof(vertex));
            }
        }

        private readonly int distanceBetweenVertices;
    }
}
