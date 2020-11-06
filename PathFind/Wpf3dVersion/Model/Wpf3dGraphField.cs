using GraphLib.GraphField;
using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex.Interface;
using System.Linq;
using System.Windows.Media.Media3D;

namespace Wpf3dVersion.Model
{
    public class Wpf3dGraphField : ModelVisual3D, IGraphField
    {
        public double DistanceBetweenAtXAxis { get; set; }
        public double DistanceBetweenAtYAxis { get; set; }
        public double DistanceBetweenAtZAxis { get; set; }

        public Wpf3dGraphField()
        {
            DistanceBetweenAtXAxis = 0;
            DistanceBetweenAtYAxis = 0;
            DistanceBetweenAtZAxis = 0;
        }

        public void Add(IVertex vertex)
        {
            SetVertexOffset(vertex as Wpf3dVertex);
            Children.Add(vertex as Wpf3dVertex);
        }

        public void SetDistanceBetweenVertices(IGraph graph)
        {
            foreach (Wpf3dVertex vertex in graph)
                SetVertexOffset(vertex);
        }

        public void CenterGraph(IGraph graph)
        {
            var dimensionSizes = graph.DimensionsSizes.ToArray();
            foreach (Wpf3dVertex vertex in graph)
            {
                var xAxisDistanceToCenter = -dimensionSizes[0] * (vertex.Size + DistanceBetweenAtXAxis) / 2;
                var yAxisDistanceToCenter = -dimensionSizes[1] * (vertex.Size + DistanceBetweenAtYAxis) / 2;
                var zAxisDistanceToCenter = -dimensionSizes[2] * (vertex.Size + DistanceBetweenAtZAxis) / 2;
                SetVertexOffset(vertex, xAxisDistanceToCenter, yAxisDistanceToCenter, zAxisDistanceToCenter);
            }
        }

        private void SetVertexOffset(Wpf3dVertex vertex, params double[] additionalOffset)
        {
            var coordinates = vertex.Position.Coordinates;
            var translate = new TranslateTransform3D
            {
                OffsetX = coordinates.ElementAt(0) * (vertex.Size + DistanceBetweenAtXAxis) + additionalOffset.ElementAtOrDefault(0),
                OffsetY = coordinates.ElementAt(1) * (vertex.Size + DistanceBetweenAtYAxis) + additionalOffset.ElementAtOrDefault(1),
                OffsetZ = coordinates.ElementAt(2) * (vertex.Size + DistanceBetweenAtZAxis) + additionalOffset.ElementAtOrDefault(2)
            };
            vertex.Transform = translate;
        }
    }
}
