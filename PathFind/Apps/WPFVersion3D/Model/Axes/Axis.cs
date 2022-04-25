using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using System.Collections.Generic;
using WPFVersion3D.Interface;

using static WPFVersion3D.Constants;

namespace WPFVersion3D.Model.Axes
{
    internal abstract class Axis : IAxis
    {
        private readonly int dimensionSize;
        private readonly IReadOnlyCollection<Vertex3D> vertices;

        protected abstract int Order { get; }

        protected Axis(int[] dimensionSizes, IReadOnlyCollection<Vertex3D> vertices)
        {
            dimensionSize = dimensionSizes[Order];
            this.vertices = (IReadOnlyCollection<Vertex3D>)vertices.ForAll(vertex => LocateVertex(vertex));
        }

        public void LocateVertices(double distanceBetweenVertices)
        {
            vertices.ForEach(vertex => LocateVertex(vertex, distanceBetweenVertices));
        }

        protected abstract void Offset(Vertex3D vertex, double offset);

        private void LocateVertex(Vertex3D vertex, double distanceBetweenVertices = 0)
        {
            int coordinate = vertex.GetCoordinates()[Order];
            double dimensionSizeCorrection = distanceBetweenVertices == 0 ? 0 : 1;
            double centeredPosition = coordinate + (dimensionSizeCorrection - dimensionSize) / 2.0;
            Offset(vertex, centeredPosition * (InitialVertexSize + distanceBetweenVertices));
        }
    }
}