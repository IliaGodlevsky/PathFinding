using Pathfinding.App.WPF._3D.Interface;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

using static Pathfinding.App.WPF._3D.Constants;

namespace Pathfinding.App.WPF._3D.Model.Axes
{
    internal abstract class Axis : IAxis
    {
        private readonly int dimensionSize;
        private readonly IEnumerable<Vertex3D> vertices;

        protected abstract int Order { get; }

        protected Axis(Graph3D<Vertex3D> graph)
        {
            dimensionSize = graph.DimensionsSizes.ElementAtOrDefault(Order);
            vertices = graph;
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