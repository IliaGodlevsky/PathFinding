using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
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
        private readonly IReadOnlyCollection<IVertex> vertices;

        protected abstract int Order { get; }

        protected Axis(Graph3D graph)
        {
            dimensionSize = graph.DimensionsSizes.ElementAtOrDefault(Order);
            vertices = graph.Vertices;
            vertices.ForEach(vertex => LocateVertex((Vertex3D)vertex));
        }

        public void LocateVertices(double distanceBetweenVertices)
        {
            vertices.ForEach(vertex => LocateVertex((Vertex3D)vertex, distanceBetweenVertices));
        }

        protected abstract void Offset(Vertex3D vertex, double offset);

        private void LocateVertex(Vertex3D vertex)
        {
            LocateVertex(vertex, distanceBetweenVertices: 0);
        }

        private void LocateVertex(Vertex3D vertex, double distanceBetweenVertices)
        {
            int coordinate = vertex.GetCoordinates().ElementAtOrDefault(Order);
            double dimensionSizeCorrection = distanceBetweenVertices == 0 ? 0 : 1;
            double centeredPosition = coordinate + (dimensionSizeCorrection - dimensionSize) / 2.0;
            Offset(vertex, centeredPosition * (InitialVertexSize + distanceBetweenVertices));
        }
    }
}