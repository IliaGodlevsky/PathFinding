using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

using static WPFVersion.Constants;

namespace WPFVersion.Model
{
    internal sealed class GraphField : Canvas, IGraphField
    {
        public IReadOnlyCollection<IVertex> Vertices => Children.Cast<IVertex>().ToArray();

        public void Add(IVertex vertex)
        {
            if (vertex is Vertex wpfVertex && wpfVertex.Position is Coordinate2D coordinates)
            {
                Children.Add(wpfVertex);
                SetLeft(wpfVertex, (DistanceBetweenVertices + wpfVertex.Width) * coordinates.X);
                SetTop(wpfVertex, (DistanceBetweenVertices + wpfVertex.Height) * coordinates.Y);
            }
        }

        public void Clear()
        {
            Children.Clear();
        }
    }
}
