using GraphLib.Interface;
using GraphLib.NullObjects;
using System;
using System.Configuration;
using System.Windows.Controls;

namespace WPFVersion.Model
{
    internal class GraphField : Canvas, IGraphField
    {
        public GraphField()
        {
            distanceBetweenVertices = Convert.ToInt32(ConfigurationManager.AppSettings["distanceBetweenVertices"]);
        }

        public void Add(IVertex vertex)
        {
            if (vertex.Position is Coordinate2D coordinates)
            {
                var wpfVertex = vertex as Vertex;
                Children.Add(wpfVertex);

                SetLeft(wpfVertex, (distanceBetweenVertices + wpfVertex.Width) * coordinates.X);
                SetTop(wpfVertex, (distanceBetweenVertices + wpfVertex.Height) * coordinates.Y);
            }
            else
            {
                var message = "An error was occured while adding vertex to a graph field\n";
                message += "Vertex must have 2D coordinate\n";
                throw new ArgumentException(message, nameof(vertex));
            }
        }

        private readonly int distanceBetweenVertices;
    }
}
