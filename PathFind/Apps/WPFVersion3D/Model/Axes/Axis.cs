using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Realizations.Graphs;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Model.Axes
{
    internal abstract class Axis : IAxis
    {
        private readonly int dimensionSize;
        private double distanceBetweenVertices = 0;

        protected abstract int Order { get; }

        protected Axis(Graph3D graph)
        {
            dimensionSize = graph.DimensionsSizes[Order];
            graph.ForEach(vertex => LocateVertex((Vertex3D)vertex));
        }

        public void Locate(GraphField3D field, double distanceBetween)
        {
            distanceBetweenVertices = distanceBetween;
            field.Vertices.ForEach(LocateVertex);
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