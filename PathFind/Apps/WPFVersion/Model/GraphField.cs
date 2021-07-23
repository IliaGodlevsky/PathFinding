using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using System.Windows.Controls;

using static WPFVersion.Constants;

namespace WPFVersion.Model
{
    internal sealed class GraphField : Canvas, IGraphField
    {
        public void Add(IVertex vertex)
        {
            if (vertex is Vertex wpfVertex && wpfVertex.Position is Coordinate2D coordinates)
            {
                Children.Add(wpfVertex);
                SetLeft(wpfVertex, (DistanceBetweenVertices + wpfVertex.Width) * coordinates.X);
                SetTop(wpfVertex, (DistanceBetweenVertices + wpfVertex.Height) * coordinates.Y);
            }
        }
    }
}
