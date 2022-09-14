using Common.Extensions.EnumerableExtensions;
using GraphLib.Realizations.Graphs;
using System.Collections.Generic;
using System.Linq;
using WPFVersion3D.Interface;

using static WPFVersion3D.Constants;

namespace WPFVersion3D.Model.Axes
{
    internal abstract class Axis : IAxis
    {
        private readonly int dimensionSize;
        private readonly IEnumerable<Vertex3D> vertices;

        protected abstract int Order { get; }

        protected Axis(Graph3D graph)
        {
            dimensionSize = graph.DimensionsSizes.ElementAtOrDefault(Order);
            vertices = graph.OfType<Vertex3D>().ToReadOnly();
            vertices.ForEach(vertex => LocateVertex(vertex));
        }

        public void LocateVertices(double distanceBetweenVertices)
        {
            vertices.ForEach(vertex => LocateVertex(vertex, distanceBetweenVertices));
        }

        protected abstract void Offset(Vertex3D vertex, double offset);

        private void LocateVertex(Vertex3D vertex, double distanceBetweenVertices = 0)
        {
            int coordinate = vertex.Position.ElementAtOrDefault(Order);
            double dimensionSizeCorrection = distanceBetweenVertices == 0 ? 0 : 1;
            double centeredPosition = coordinate + (dimensionSizeCorrection - dimensionSize) / 2.0;
            Offset(vertex, centeredPosition * (InitialVertexSize + distanceBetweenVertices));
        }
    }
}