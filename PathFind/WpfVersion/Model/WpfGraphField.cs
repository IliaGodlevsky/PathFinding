using Common;
using GraphLib.Coordinates;
using GraphLib.GraphField;
using GraphLib.Vertex.Interface;
using System;
using System.Windows.Controls;
using WPFVersion.Model.Vertex;

namespace WPFVersion.Model
{
    internal class WpfGraphField : Canvas, IGraphField
    {
        public void Add(IVertex vertex)
        {
            if (vertex.Position is Coordinate2D coordinates)
            {
                var wpfVertex = vertex as WpfVertex;
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
