using Common.Extensions;
using GraphLib.GraphField;
using GraphLib.Vertex.Interface;
using System.Linq;
using System.Windows.Media.Media3D;
using WPFVersion3D.Enums;
using DistanceBetweenSetCallback = System.Action<double, WPFVersion3D.Model.GraphField3D>;
using OffsetAction = System.Action<System.Windows.Media.Media3D.TranslateTransform3D, double>;

namespace WPFVersion3D.Model
{
    internal class GraphField3D : ModelVisual3D, IGraphField
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
            DistanceBetweenVerticesAtXAxis = 2;
            DistanceBetweenVerticesAtYAxis = 2;
            DistanceBetweenVerticesAtZAxis = 2;
        }

        public void Add(IVertex vertex)
        {
            Vertex3D vertex3D = vertex as Vertex3D;

            foreach (var axis in Axes)
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
                    LocateVertex(Axes[i], vertex, axisOffsets);
                }
            }
        }

        public void StretchAlongAxis(Axis axis, double distanceBetween,
            params double[] additionalOffset)
        {
            int axisIndex = axis.GetValue<int>();
            DistanceBetweenSetters[axisIndex](distanceBetween, this);
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
            int axisIndex = axis.GetValue<int>();
            var coordinates = vertex.Position.CoordinatesValues;
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

        private Axis[] Axes => new Axis[]
        {
            Axis.Abscissa,
            Axis.Ordinate,
            Axis.Applicate
        };
    }
}
