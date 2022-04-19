using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using System.Collections.Generic;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Model.Axes
{
    internal abstract class Axis : IAxis
    {
        private readonly int dimensionSize;
        private readonly IReadOnlyCollection<Vertex3D> vertices;

        private double distanceBetweenVertices = 0;

        protected abstract int Order { get; }

        protected Axis(int[] dimensionSizes, IReadOnlyCollection<Vertex3D> vertices)
        {
            dimensionSize = dimensionSizes[Order];
            this.vertices = vertices;
            this.vertices.ForEach(LocateVertex);
        }

        public void LocateVertices(double distanceBetweenVertices)
        {
            this.distanceBetweenVertices = distanceBetweenVertices;
            vertices.ForEach(LocateVertex);
        }

        protected abstract void Offset(Vertex3D vertex, double offset);

        private void LocateVertex(Vertex3D vertex)
        {
            var coordinates = vertex.GetCoordinates();
            double adjustedVertexSize = Constants.InitialVertexSize + distanceBetweenVertices;
            double dimensionSizeCorrection = distanceBetweenVertices == 0 ? 0 : 1;
            double centeredPosition = coordinates[Order] + (dimensionSizeCorrection - dimensionSize) / 2.0;
            Offset(vertex, centeredPosition * adjustedVertexSize);
        }
    }
}