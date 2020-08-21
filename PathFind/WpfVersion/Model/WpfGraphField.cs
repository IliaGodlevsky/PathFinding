using GraphLibrary.Constants;
using GraphLibrary.Model;
using GraphLibrary.Vertex;
using System.Windows.Controls;
using WpfVersion.Model.Vertex;

namespace WpfVersion.Model
{
    public class WpfGraphField : Canvas, IGraphField
    {
        public void Add(IVertex vertex, int xCoordinate, int yCoordinate)
        {
            Children.Add(vertex as WpfVertex);
            SetLeft(vertex as WpfVertex, Const.SIZE_BETWEEN_VERTICES * xCoordinate);
            SetTop(vertex as WpfVertex, Const.SIZE_BETWEEN_VERTICES * yCoordinate);
        }
    }
}
