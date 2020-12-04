using GraphLib.GraphField;
using GraphLib.Vertex.Interface;
using System.Linq;
using System.Windows.Media.Media3D;
using OffsetAction = System.Action<System.Windows.Media.Media3D.TranslateTransform3D, double>;
using DistanceBetweenSetCallback = System.Action<double, Wpf3dVersion.Model.WpfGraphField3D>;
using Wpf3dVersion.Enums;
using Common.Extensions;

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
            LocateVertex(vertex3D);
            Children.Add(vertex3D);
        }

        public void StretchAlongAxes()
        {
            foreach (WpfVertex3D vertex in Children)
            {
                LocateVertex(vertex);
            }
        }

        public void CenterGraph(params double[] additionalOffset)
        {
            var dimensionSizes = new int[] { Width, Length, Height };
            var axisOffsets = new double[dimensionSizes.Length];
            foreach (WpfVertex3D vertex in Children)
            {
                for (int i = 0; i < dimensionSizes.Length; i++)
                {
                    var graphOffset = new Offset
                    {
                        DimensionSize = dimensionSizes.ElementAtOrDefault(i),
                        VertexSize = vertex.Size,
                        AdditionalOffset = additionalOffset.ElementAtOrDefault(i),
                        DistanceBetweenVertices = DistanceBetween.ElementAtOrDefault(i)
                    };
                    axisOffsets[i] = graphOffset.GraphCenterOffset;
                }
                LocateVertex(vertex, axisOffsets);
            }
        }

        public void StretchAlongAxis(Axis axis, double distanceBetween, 
            params double[] additionalOffset)
        {
            int axisIndex = axis.GetValue();
            DistanceBetweenSetters[axisIndex](distanceBetween, this);
            StretchAlongAxes();

            if (distanceBetween == 0)
                CenterGraph(0, 0, 0);
            else
                CenterGraph(additionalOffset);
        }

        private void LocateVertex(WpfVertex3D vertex, 
            params double[] additionalOffset)
        {            
            var coordinates = vertex.Position.Coordinates;
            var translate = new TranslateTransform3D();
            for (int i = 0; i < coordinates.Count(); i++)
            {
                var vertexOffset = new Offset
                {
                    CoordinateValue = coordinates.ElementAtOrDefault(i),
                    VertexSize = vertex.Size,
                    DistanceBetweenVertices = DistanceBetween.ElementAtOrDefault(i),
                    AdditionalOffset = additionalOffset.ElementAtOrDefault(i)
                };
                var offset = vertexOffset.VertexOffset;
                OffsetActions[i](translate, offset);
            }
            vertex.Transform = translate;
        }

        private int Width { get; set; }

        private int Length { get; set; }

        private int Height { get; set; }

        private double[] DistanceBetween => new double[] 
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
    }
}
