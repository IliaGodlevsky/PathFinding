using Autofac;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Media3D;
using WPFVersion3D.Axes;
using WPFVersion3D.DependencyInjection;

namespace WPFVersion3D.Model
{
    internal sealed class GraphField3D : ModelVisual3D, IGraphField
    {
        private readonly IReadOnlyCollection<Vertex3D> vertices;

        public IReadOnlyCollection<IVertex> Vertices => vertices;

        public double DistanceBetweenVerticesAtXAxis { get; set; } = default;

        public double DistanceBetweenVerticesAtYAxis { get; set; } = default;

        public double DistanceBetweenVerticesAtZAxis { get; set; } = default;

        private int[] DimensionSizes { get; }

        private IAxis[] CoordinateSystem { get; }

        private double[] DistancesBetween => new[]
        {
            DistanceBetweenVerticesAtZAxis,
            DistanceBetweenVerticesAtYAxis,
            DistanceBetweenVerticesAtXAxis
        };

        public GraphField3D(Graph3D graph)
        {
            CoordinateSystem = DI.Container
                .Resolve<IEnumerable<IAxis>>()
                .OrderBy(axis => axis.Order)
                .ToArray();
            DimensionSizes = graph.DimensionsSizes;
            vertices = graph.Vertices.OfType<Vertex3D>().ToArray();
            Children.AddRange(vertices);
            vertices.ForEach(vertex => LocateVertex(CoordinateSystem, vertex));
        }

        public GraphField3D()
        {

        }

        public void CenterGraph(params double[] offsets)
        {
            var centerOffsets = CoordinateSystem.Select(axis => CalculateAxisOffset(offsets, axis)).ToArray();
            vertices.ForEach(vertex => LocateVertex(CoordinateSystem, vertex, centerOffsets));
        }

        public void StretchAlongAxis(IAxis axis, double distanceBetween, params double[] additionalOffset)
        {
            axis.SetDistanceBeetween(distanceBetween, this);
            StretchAlongAxis(axis);
            CenterGraph(distanceBetween == 0 ? Array.Empty<double>() : additionalOffset);
        }

        private double CalculateAxisOffset(double[] additionalOffset, IAxis axis)
        {
            return (additionalOffset.ElementAtOrDefault(axis.Order) - DimensionSizes[axis.Order]) * GetAdjustedVertexSize(axis) / 2;
        }

        private void StretchAlongAxis(IAxis axis)
        {
            Vertices.ForEach(vertex => LocateVertex(axis, (Vertex3D)vertex));
        }

        private void LocateVertex(IAxis[] axes, Vertex3D vertex, params double[] additionalOffset)
        {
            axes.ForEach(axis => LocateVertex(axis, vertex, additionalOffset));
        }

        private void LocateVertex(IAxis axis, Vertex3D vertex, params double[] additionalOffset)
        {
            var coordinates = vertex.GetCoordinates();
            double offset = GetAdjustedVertexSize(axis) * coordinates[axis.Order]
                + additionalOffset.ElementAtOrDefault(axis.Order);
            axis.Offset(vertex.Transform as TranslateTransform3D, offset);
        }

        private double GetAdjustedVertexSize(IAxis axis)
        {
            return Constants.InitialVertexSize + DistancesBetween[axis.Order];
        }
    }
}