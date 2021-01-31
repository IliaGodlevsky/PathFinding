using Common;
using GraphLib.Coordinates;
using GraphLib.GraphField;
using GraphLib.Vertex.Interface;
using System;
using System.Configuration;
using System.Windows.Controls;

namespace WPFVersion.Model
{
    internal class GraphField : Canvas, IGraphField
    {
        public GraphField()
        {
            distanceBetweenVertices
                    = Convert.ToInt32(ConfigurationManager.AppSettings["distanceBetweenVertices"])
                    + Convert.ToInt32(ConfigurationManager.AppSettings["vertexSize"]);
        }

        public void Add(IVertex vertex)
        {
            if (vertex.Position is Coordinate2D coordinates)
            {
                var wpfVertex = vertex as Vertex;
                Children.Add(wpfVertex);

                SetLeft(wpfVertex, distanceBetweenVertices * coordinates.X);
                SetTop(wpfVertex, distanceBetweenVertices * coordinates.Y);
            }
            else
            {
                throw new ArgumentException("Must be 2D coordinates");
            }
        }

        private readonly int distanceBetweenVertices;
    }
}
