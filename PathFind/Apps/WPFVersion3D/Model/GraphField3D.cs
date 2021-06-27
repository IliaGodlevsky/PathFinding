using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Media3D;
using WPFVersion3D.Axes;

namespace WPFVersion3D.Model
{
    internal sealed class GraphField3D : ModelVisual3D, IGraphField
    {
        public double DistanceBetweenVerticesAtXAxis { get; set; }

        public double DistanceBetweenVerticesAtYAxis { get; set; }

        public double DistanceBetweenVerticesAtZAxis { get; set; }

        public IReadOnlyCollection<IVertex> Vertices => Children.OfType<IVertex>().ToArray();

        public GraphField3D(int width, int length, int height)
        {
            coordinateSystem = new IAxis[]
            {
                new Abscissa(),
                new Ordinate(),
                new Applicate()
            };

            Width = width;
            Length = length;
            Height = height;

            DimensionSizes = new[] { Width, Length, Height };

            DistanceBetweenVerticesAtXAxis = 0;
            DistanceBetweenVerticesAtYAxis = 0;
            DistanceBetweenVerticesAtZAxis = 0;
        }

        public void Add(IVertex vertex)
        {
            if (vertex is Vertex3D vertex3D && vertex.Position is Coordinate3D)
            {
                foreach (var axis in coordinateSystem)
                {
                    LocateVertex(axis, vertex3D);
                }

                Children.Add(vertex3D);
            }
        }

        public void Clear()
        {
            Children.Clear();
        }

        public void CenterGraph(params double[] additionalOffset)
        {
            var axisOffsets = new double[DimensionSizes.Length];
            var vertices = Children.Cast<Vertex3D>();
            foreach (var vertex in vertices)
            {
                for (int i = 0; i < DimensionSizes.Length; i++)
                {
                    var graphOffset = new Offset
                    {
                        DimensionSize = DimensionSizes[i],
                        VertexSize = vertex.Size,
                        AdditionalOffset = additionalOffset.ElementAtOrDefault(i),
                        DistanceBetweenVertices = DistancesBetween[i]
                    };
                    axisOffsets[i] = graphOffset.GraphCenterOffset;
                    LocateVertex(coordinateSystem[i], vertex, axisOffsets);
                }
            }
        }

        public void StretchAlongAxis(IAxis axis, double distanceBetween,
            params double[] additionalOffset)
        {
            axis.SetDistanceBeetween(distanceBetween, this);
            StretchAlongAxes(axis);

            if (distanceBetween == 0)
            {
                CenterGraph();
            }
            else
            {
                CenterGraph(additionalOffset);
            }
        }

        private void StretchAlongAxes(params IAxis[] axes)
        {
            var vertices = Vertices.Cast<Vertex3D>();
            foreach (var vertex in vertices)
            {
                foreach (var axis in axes)
                {
                    LocateVertex(axis, vertex);
                }
            }
        }

        private void LocateVertex(IAxis axis, Vertex3D vertex,
            params double[] additionalOffset)
        {
            var coordinates = vertex.Position.CoordinatesValues;
            var vertexOffset = new Offset
            {
                CoordinateValue = coordinates[axis.Index],
                DistanceBetweenVertices = DistancesBetween[axis.Index],
                AdditionalOffset = additionalOffset.ElementAtOrDefault(axis.Index),
                VertexSize = vertex.Size
            };
            double offset = vertexOffset.VertexOffset;

            if (!(vertex.Transform is TranslateTransform3D transform))
            {
                string paramName = nameof(vertex.Transform);
                string requiredType = nameof(TranslateTransform3D);
                string message = $"{paramName} is not of type {requiredType}";
                throw new Exception(message);
            }

            axis.Offset(transform, offset);
        }

        private int Width { get; }

        private int Length { get; }

        private int Height { get; }

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
