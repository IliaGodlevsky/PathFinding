using GraphLibrary.Common.Constants;
using GraphLibrary.GraphField;
using GraphLibrary.Vertex.Interface;
using System.Windows.Controls;
using WpfVersion.Model.Vertex;

namespace WpfVersion.Model
{
    internal class WpfGraphField : Canvas, IGraphField
    {
        public void Add(IVertex vertex, int xCoordinate, int yCoordinate)
        {
            Children.Add(vertex as WpfVertex);
            SetLeft(vertex as WpfVertex, VertexSize.SIZE_BETWEEN_VERTICES * xCoordinate);
            SetTop(vertex as WpfVertex, VertexSize.SIZE_BETWEEN_VERTICES * yCoordinate);
        }
    }
}
