using GraphLib.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Media3D;
using WPFVersion3D.Enums;
using DistanceBetweenVerticesSetterCallback = System.Action<double, WPFVersion3D.Model.GraphField3D>;
using OffsetSetterAction = System.Action<System.Windows.Media.Media3D.TranslateTransform3D, double>;

namespace WPFVersion3D.Model
{
    internal sealed class GraphField3D : ModelVisual3D, IGraphField
    {
        public double DistanceBetweenVerticesAtXAxis { get; set; }

        public double DistanceBetweenVerticesAtYAxis { get; set; }

        public double DistanceBetweenVerticesAtZAxis { get; set; }

        public GraphField3D(int width, int length, int height) : this()
        {
            Width = width;
            Length = length;
            Height = height;
        }

        public GraphField3D()
        {
            axes = Enum.GetValues(typeof(Axis)).Cast<Axis>().ToArray();

            offsetActions = new Dictionary<Axis, OffsetSetterAction>
            {
                { Axis.Abscissa, (transform, offset) => { transform.OffsetX = offset; } },
                { Axis.Ordinate, (transform, offset) => { transform.OffsetY = offset; } },
                { Axis.Applicate,(transform, offset) => { transform.OffsetZ = offset; } }
            };

            distanceBetweenVerticesSetters = new Dictionary<Axis, DistanceBetweenVerticesSetterCallback>
            {
                { Axis.Abscissa, (distanceBetween, field) => field.DistanceBetweenVerticesAtXAxis = distanceBetween },
                { Axis.Ordinate, (distanceBetween, field) => field.DistanceBetweenVerticesAtYAxis = distanceBetween },
                { Axis.Applicate, (distanceBetween, field) => field.DistanceBetweenVerticesAtZAxis = distanceBetween }
            };

            DistanceBetweenVerticesAtXAxis = 0;
            DistanceBetweenVerticesAtYAxis = 0;
            DistanceBetweenVerticesAtZAxis = 0;
        }

        public void Add(IVertex vertex)
        {
            Vertex3D vertex3D = vertex as Vertex3D;

            foreach (var axis in axes)
            {
                LocateVertex(axis, vertex3D);
            }

            Children.Add(vertex3D);
        }

        public void CenterGraph(params double[] additionalOffset)
        {
            var axisOffsets = new double[DimensionSizes.Length];
            foreach (Vertex3D vertex in Children)
            {
                for (int i = 0; i < DimensionSizes.Length; i++)
                {
                    var graphOffset = new Offset
                    {
                        DimensionSize = DimensionSizes.ElementAtOrDefault(i),
                        VertexSize = vertex.Size,
                        AdditionalOffset = additionalOffset.ElementAtOrDefault(i),
                        DistanceBetweenVertices = DistancesBetween.ElementAtOrDefault(i)
                    };
                    axisOffsets[i] = graphOffset.GraphCenterOffset;
                    LocateVertex(axes[i], vertex, axisOffsets);
                }
            }
        }

        public void StretchAlongAxis(Axis axis, double distanceBetween,
            params double[] additionalOffset)
        {
            distanceBetweenVerticesSetters[axis](distanceBetween, this);
            StretchAlongAxes(axis);

            if (distanceBetween == 0)
                CenterGraph(0, 0, 0);
            else
                CenterGraph(additionalOffset);
        }

        private void StretchAlongAxes(params Axis[] axes)
        {
            foreach (Vertex3D vertex in Children)
            {
                foreach (var axis in axes)
                {
                    LocateVertex(axis, vertex);
                }
            }
        }

        private void LocateVertex(Axis axis, Vertex3D vertex,
            params double[] additionalOffset)
        {
            int axisIndex = Array.IndexOf(axes, axis);
            var coordinates = vertex.Position.CoordinatesValues;
            var vertexOffset = new Offset
            {
                CoordinateValue = coordinates.ElementAtOrDefault(axisIndex),                
                DistanceBetweenVertices = DistancesBetween.ElementAtOrDefault(axisIndex),
                AdditionalOffset = additionalOffset.ElementAtOrDefault(axisIndex),
                VertexSize = vertex.Size
            };
            double offset = vertexOffset.VertexOffset;
            var transform = vertex.Transform as TranslateTransform3D;
            offsetActions[axis](transform, offset);
        }

        private int Width { get; set; }

        private int Length { get; set; }

        private int Height { get; set; }

        private int[] DimensionSizes => new int[]
        {
            Width, Length, Height
        };

        private double[] DistancesBetween => new double[]
        {
            DistanceBetweenVerticesAtXAxis,
            DistanceBetweenVerticesAtYAxis,
            DistanceBetweenVerticesAtZAxis
        };

        private readonly Dictionary<Axis,OffsetSetterAction> offsetActions;
        private readonly Dictionary<Axis, DistanceBetweenVerticesSetterCallback> distanceBetweenVerticesSetters;
        private readonly Axis[] axes;
    }
}
