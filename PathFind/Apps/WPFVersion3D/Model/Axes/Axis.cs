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
            graph.ForEach<Vertex3D>(LocateVertex);
        }

        public void Locate(GraphField3D field, double distanceBetweenVertices)
        {
            this.distanceBetweenVertices = distanceBetweenVertices;
            field.Vertices.ForEach(LocateVertex);
        }

        protected abstract void Offset(Vertex3D vertex, double offset);

        private void LocateVertex(Vertex3D vertex)
        {
            var coordinates = vertex.GetCoordinates();
            double adjustedVertexSize = Constants.InitialVertexSize + distanceBetweenVertices;
            double centeredPosition = (coordinates[Order] - dimensionSize / 2.0);
            Offset(vertex, centeredPosition * adjustedVertexSize);
        }
    }
}