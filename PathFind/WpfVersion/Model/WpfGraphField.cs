using Common;
using GraphLib.Coordinates;
using GraphLib.GraphField;
using GraphLib.Vertex.Interface;
using System;
using System.Windows.Controls;
using WpfVersion.Model.Vertex;

namespace WpfVersion.Model
{
    internal class WpfGraphField : Canvas, IGraphField
    {
        public void Add(IVertex vertex)
        {
            if (vertex.Position is Coordinate2D coordinates)
            {
                Children.Add(vertex as WpfVertex);

                SetLeft(vertex as WpfVertex, VertexParametres.SizeBetweenVertices * coordinates.X);
                SetTop(vertex as WpfVertex, VertexParametres.SizeBetweenVertices * coordinates.Y);
            }
            else
            {
                throw new ArgumentException("Must be 2D coordinates");
            }
        }
    }
}
