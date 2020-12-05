using GraphLib.GraphField;
using GraphLib.Vertex.Interface;
using System.Linq;
using System.Windows.Media.Media3D;
using OffsetAction = System.Action<System.Windows.Media.Media3D.TranslateTransform3D, double>;
using DistanceBetweenSetCallback = System.Action<double, Wpf3dVersion.Model.WpfGraphField3D>;
using Wpf3dVersion.Enums;
using Common.Extensions;
using System;

namespace Wpf3dVersion.Model
{
    internal class WpfGraphField3D : ModelVisual3D, IGraphField
    {
        public double DistanceBetweenVerticesAtXAxis { get; set; }

        public double DistanceBetweenVerticesAtYAxis { get; set; }

        public double DistanceBetweenVerticesAtZAxis { get; set; }

        public WpfGraphField3D(int width,
            int length, int height) : this()
        {
            Width = width;
            Length = length;
            Height = height;
        }

        public WpfGraphField3D()
        {
            DistanceBetweenVerticesAtXAxis = 0;
            DistanceBetweenVerticesAtYAxis = 0;
            DistanceBetweenVerticesAtZAxis = 0;
        }

        public void Add(IVertex vertex)
        {
            WpfVertex3D vertex3D = vertex as WpfVertex3D;

            foreach (var axis in Axes)
            {
                LocateVertex(axis, vertex3D);
            }

            Children.Add(vertex3D);
        }

        public void CenterGraph(params double[] additionalOffset)
        {
            var axisOffsets = new double[DimensionSizes.Length];
            foreach (WpfVertex3D vertex in Children)
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
                    LocateVertex(Axes[i], vertex, axisOffsets);
                }                
            }
        }

        public void StretchAlongAxis(Axis axis, double distanceBetween,
            params double[] additionalOffset)
        {
            int axisIndex = axis.GetValue();
            DistanceBetweenSetters[axisIndex](distanceBetween, this);
            StretchAlongAxes(axis);

            if (distanceBetween == 0)
                CenterGraph(0, 0, 0);
            else
                CenterGraph(additionalOffset);
        }

        private void StretchAlongAxes(params Axis[] axes)
        {
            foreach (WpfVertex3D vertex in Children)
            {
                foreach (var axis in axes)
                {
                    LocateVertex(axis, vertex);
                }
            }
        }

        private void LocateVertex(Axis axis, WpfVertex3D vertex,
            params double[] additionalOffset)
        {
            int axisIndex = axis.GetValue();
            var coordinates = vertex.Position.Coordinates;
            var vertexOffset = new Offset
            {
                CoordinateValue = coordinates.ElementAtOrDefault(axisIndex),
                VertexSize = vertex.Size,
                DistanceBetweenVertices = DistancesBetween.ElementAtOrDefault(axisIndex),
                AdditionalOffset = additionalOffset.ElementAtOrDefault(axisIndex)
            };
            var offset = vertexOffset.VertexOffset;
            OffsetActions[axisIndex](vertex.Transform as TranslateTransform3D, offset);
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

        private OffsetAction[] OffsetActions => new OffsetAction[]
        {
            (transform, offset) => { transform.OffsetX = offset; },
            (transform, offset) => { transform.OffsetY = offset; },
            (transform, offset) => { transform.OffsetZ = offset; }
        };

        private DistanceBetweenSetCallback[] DistanceBetweenSetters => new DistanceBetweenSetCallback[]
        {
            (distanceBetween, field) => field.DistanceBetweenVerticesAtXAxis = distanceBetween,
            (distanceBetween, field) => field.DistanceBetweenVerticesAtYAxis = distanceBetween,
            (distanceBetween, field) => field.DistanceBetweenVerticesAtZAxis = distanceBetween
        };

        private Axis[] Axes => Enum.GetValues(typeof(Axis)).Cast<Axis>().ToArray();
    }
}
