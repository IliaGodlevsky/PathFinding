using GraphLibrary.Coordinates;
using GraphLibrary.Globals;
using GraphLibrary.GraphField;
using GraphLibrary.Vertex.Interface;
using System;
using System.Windows.Controls;
using WpfVersion.Model.Vertex;

namespace WpfVersion.Model
{
    internal class WpfGraphField : Canvas, IGraphField
    {
        public void Add(IVertex vertex)
        {
            var coordinates = vertex.Position as Coordinate2D;
            if (coordinates == null)
                throw new ArgumentException("Must be 2D coordinates");
            Children.Add(vertex as WpfVertex);
            SetLeft(vertex as WpfVertex, VertexParametres.SizeBetweenVertices * coordinates.X);
            SetTop(vertex as WpfVertex, VertexParametres.SizeBetweenVertices * coordinates.Y);
        }
    }
}
