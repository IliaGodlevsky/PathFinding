using Common.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

using static WPFVersion.Constants;

namespace WPFVersion.Model
{
    internal sealed class GraphField : Canvas, IGraphField
    {
        public double VerticesSize { get; set; }

        public IReadOnlyCollection<IVertex> Vertices => Children.Cast<IVertex>().ToArray();

        public GraphField()
        {
            VerticesSize = VertexSize;
        }

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

        public void Rearrange(double vertexSize)
        {
            VerticesSize = vertexSize;
            double scale = VerticesSize / VertexSize;
            var scaleTransform = new ScaleTransform
            {
                ScaleX = scale,
                ScaleY = scale
            };
            var groupTransform = new TransformGroup();
            groupTransform.Children.AddRange(scaleTransform);
            RenderTransform = groupTransform;
        }
    }
}
