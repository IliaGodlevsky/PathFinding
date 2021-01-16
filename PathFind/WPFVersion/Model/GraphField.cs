using Common;
using GraphLib.Coordinates;
using GraphLib.GraphField;
using GraphLib.Vertex.Interface;
using System;
using System.Windows.Controls;

namespace WPFVersion.Model
{
    internal class GraphField : Canvas, IGraphField
    {
        public void Add(IVertex vertex)
        {
            if (vertex.Position is Coordinate2D coordinates)
            {
                var wpfVertex = vertex as Vertex;
                Children.Add(wpfVertex);

                SetLeft(wpfVertex, VertexParametres.SizeBetweenVertices * coordinates.X);
                SetTop(wpfVertex, VertexParametres.SizeBetweenVertices * coordinates.Y);
            }
            else
            {
                throw new ArgumentException("Must be 2D coordinates");
            }
        }
    }
}
