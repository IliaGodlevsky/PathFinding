using GraphLib.GraphField;
using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex.Interface;
using System.Linq;
using System.Windows.Media.Media3D;

namespace Wpf3dVersion.Model
{
    public class Wpf3dGraphField : ModelVisual3D, IGraphField
    {
        private readonly byte[] correctionOffset;
        public double DistanceBetween { get; set; }

        public void IsolateXAxis() => correctionOffset[0] = 0;
        public void ReturnXAxis() => correctionOffset[0] = 1;

        public void IsolateYAxis() => correctionOffset[1] = 0;
        public void ReturnYAxis() => correctionOffset[1] = 1;

        public void IsolateZAxis() => correctionOffset[2] = 0;
        public void ReturnZAxis() => correctionOffset[2] = 1;

        public Wpf3dGraphField()
        {
            DistanceBetween = 2;
            correctionOffset = new byte[3] { 1, 1, 1 };
        }

        public void Add(IVertex vertex)
        {
            SetVertexOffset(vertex as Wpf3dVertex, DistanceBetween);
            Children.Add(vertex as Wpf3dVertex);
        }

        public void SetDistanceBetweenVertices(IGraph graph)
        {
            foreach (Wpf3dVertex vertex in graph)
                SetVertexOffset(vertex, DistanceBetween);
        }

        public void CenterGraph(IGraph graph)
        {
            var sizes = graph.DimensionsSizes.ToArray();
            foreach (Wpf3dVertex vertex in graph)
            {
                for (int i = 0; i < sizes.Length; i++)
                {
                    var correction = -sizes[i] * (vertex.Size + DistanceBetween) / 2;
                    SetVertexOffset(vertex, correction);
                }
            }
        }

        private void SetVertexOffset(Wpf3dVertex vertex, double offsetCorrection = 0.0)
        {
            var coordinates = vertex.Position.Coordinates;
            var translate = new TranslateTransform3D
            {
                OffsetX = coordinates.First() * (vertex.Size + DistanceBetween * correctionOffset[0]) + offsetCorrection,
                OffsetY = coordinates.ElementAt(1) * (vertex.Size + DistanceBetween * correctionOffset[1]) + offsetCorrection,
                OffsetZ = coordinates.Last() * (vertex.Size + DistanceBetween * correctionOffset[2]) + offsetCorrection
            };
            vertex.Transform = translate;
        }
    }
}
