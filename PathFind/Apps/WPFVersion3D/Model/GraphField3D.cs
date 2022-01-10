using Autofac;
using Common.Extensions;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using System;
using System.Linq;
using System.Windows.Media.Media3D;
using WPFVersion3D.Axes;
using WPFVersion3D.DependencyInjection;

namespace WPFVersion3D.Model
{
    internal sealed class GraphField3D : ModelVisual3D, IGraphField
    {
        public double DistanceBetweenVerticesAtXAxis { get; set; } = default;
        public double DistanceBetweenVerticesAtYAxis { get; set; } = default;
        public double DistanceBetweenVerticesAtZAxis { get; set; } = default;

        public GraphField3D(int width, int length, int height)
        {
            CoordinateSystem = DI.Container.Resolve<IAxis[]>();
            DimensionSizes = new[] { width, length, height };
            AxisIndices = Enumerable.Range(0, CoordinateSystem.Length).ToArray();
        }

        public void Add(IVertex vertex)
        {
            if (vertex is Vertex3D vertex3D && vertex.Position is Coordinate3D)
            {
                LocateVertex(CoordinateSystem, vertex3D);
                Children.Add(vertex3D);
            }
        }

        public void CenterGraph(params double[] offsets)
        {
            var centerOffsets = AxisIndices.Select(i => CalculateAxisOffset(offsets, i)).ToArray();
            Children.ForEach(vertex => LocateVertex(CoordinateSystem, (Vertex3D)vertex, centerOffsets));
        }

        public void StretchAlongAxis(IAxis axis, double distanceBetween, params double[] additionalOffset)
        {
            axis.SetDistanceBeetween(distanceBetween, this);
            StretchAlongAxis(axis);
            CenterGraph(distanceBetween == 0 ? Array.Empty<double>() : additionalOffset);
        }

        private double CalculateAxisOffset(double[] additionalOffset, int index)
        {
            return (additionalOffset.ElementAtOrDefault(index) - DimensionSizes[index]) * GetAdjustedVertexSize(index) / 2;
        }

        private void StretchAlongAxis(IAxis axis)
        {
            Children.ForEach(vertex => LocateVertex(axis, (Vertex3D)vertex));
        }

        private void LocateVertex(IAxis[] axes, Vertex3D vertex, params double[] additionalOffset)
        {
            axes.ForEach(axis => LocateVertex(axis, vertex, additionalOffset));
        }

        private void LocateVertex(IAxis axis, Vertex3D vertex, params double[] additionalOffset)
        {
            var coordinates = vertex.GetCoordinates();
            double offset = GetAdjustedVertexSize(axis.Order) * coordinates[axis.Order]
                + additionalOffset.ElementAtOrDefault(axis.Order);
            axis.Offset(vertex.Transform as TranslateTransform3D, offset);
        }

        private double GetAdjustedVertexSize(int index) => Constants.InitialVertexSize + DistancesBetween[index];

        private int[] AxisIndices { get; }
        private int[] DimensionSizes { get; }
        private IAxis[] CoordinateSystem { get; }

        private double[] DistancesBetween => new[]
        {
            DistanceBetweenVerticesAtZAxis,
            DistanceBetweenVerticesAtYAxis,
            DistanceBetweenVerticesAtXAxis
        };
    }
}