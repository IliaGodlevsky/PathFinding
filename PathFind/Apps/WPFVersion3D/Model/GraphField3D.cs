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
        public double DistanceBetweenVerticesAtXAxis { get; set; }

        public double DistanceBetweenVerticesAtYAxis { get; set; }

        public double DistanceBetweenVerticesAtZAxis { get; set; }

        public GraphField3D(int width, int length, int height)
        {
            coordinateSystem = new IAxis[] { new Applicate(), new Ordinate(), new Abscissa() };
            DimensionSizes = new[] { width, length, height };
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

        public void CenterGraph(params double[] additionalOffset)
        {
            var axisOffsets = new double[DimensionSizes.Length];
            foreach (Vertex3D vertex in Children)
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
            StretchAlongAxis(axis);

            if (distanceBetween == 0)
            {
                CenterGraph();
            }
            else
            {
                CenterGraph(additionalOffset);
            }
        }

        private void StretchAlongAxis(IAxis axis)
        {
            foreach (Vertex3D vertex in Children)
            {
                LocateVertex(axis, vertex);
            }
        }

        private void LocateVertex(IAxis axis, Vertex3D vertex,
            params double[] additionalOffset)
        {
            var coordinates = vertex.GetCoordinates();
            var vertexOffset = new Offset
            {
                CoordinateValue = coordinates[axis.Index],
                DistanceBetweenVertices = DistancesBetween[axis.Index],
                AdditionalOffset = additionalOffset.ElementAtOrDefault(axis.Index),
                VertexSize = vertex.Size
            };
            double offset = vertexOffset.VertexOffset;
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
