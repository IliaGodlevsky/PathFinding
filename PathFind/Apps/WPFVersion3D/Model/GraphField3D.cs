using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using System.Linq;
using System.Windows.Media.Media3D;
using WPFVersion3D.Axes;

namespace WPFVersion3D.Model
{
    internal sealed class GraphField3D : ModelVisual3D, IGraphField
    {
        private const int Close = default;

        public double DistanceBetweenVerticesAtXAxis { get; set; } = default;
        public double DistanceBetweenVerticesAtYAxis { get; set; } = default;
        public double DistanceBetweenVerticesAtZAxis { get; set; } = default;

        public GraphField3D(int width, int length, int height)
        {
            coordinateSystem = new IAxis[] { new Applicate(), new Ordinate(), new Abscissa() };
            DimensionSizes = new[] { width, length, height };
        }

        public void Add(IVertex vertex)
        {
            if (vertex is Vertex3D vertex3D && vertex.Position is Coordinate3D)
            {
                LocateVertex(coordinateSystem, vertex3D);
                Children.Add(vertex3D);
            }
        }

        public void CenterGraph(params double[] additionalOffset)
        {
            var axisOffsets = new double[DimensionSizes.Length];
            foreach (Vertex3D vertex in Children)
            {
                for (int i = 0; i < DimensionSizes.Length; i++)
                {
                    double addOffset = additionalOffset.ElementAtOrDefault(i);
                    double adjustedDimensionSize = (addOffset - DimensionSizes[i]);
                    double adjustedVertexSize = vertex.Size + DistancesBetween[i];
                    axisOffsets[i] = adjustedDimensionSize * adjustedVertexSize / 2;
                }
                LocateVertex(coordinateSystem, vertex, axisOffsets);
            }
        }

        public void StretchAlongAxis(IAxis axis, double distanceBetween, params double[] additionalOffset)
        {
            axis.SetDistanceBeetween(distanceBetween, this);
            StretchAlongAxis(axis);
            switch (distanceBetween)
            {
                case Close: CenterGraph(); break;
                default: CenterGraph(additionalOffset); break;
            }
        }

        private void StretchAlongAxis(IAxis axis)
        {
            foreach (Vertex3D vertex in Children)
            {
                LocateVertex(axis, vertex);
            }
        }

        private void LocateVertex(IAxis[] axes, Vertex3D vertex, params double[] additionalOffset)
        {
            foreach (var axis in axes)
            {
                LocateVertex(axis, vertex, additionalOffset);
            }
        }

        private void LocateVertex(IAxis axis, Vertex3D vertex, params double[] additionalOffset)
        {
            var coordinates = vertex.GetCoordinates();
            double addOffset = additionalOffset.ElementAtOrDefault(axis.Index);
            double adjustedVertexSize = vertex.Size + DistancesBetween[axis.Index];
            double offset = (adjustedVertexSize * coordinates[axis.Index]) + addOffset;
            axis.Offset(vertex.Transform as TranslateTransform3D, offset);
        }

        private int[] DimensionSizes { get; }

        private double[] DistancesBetween => new[]
        {
            DistanceBetweenVerticesAtXAxis,
            DistanceBetweenVerticesAtYAxis,
            DistanceBetweenVerticesAtZAxis
        };

        private readonly IAxis[] coordinateSystem;
    }
}