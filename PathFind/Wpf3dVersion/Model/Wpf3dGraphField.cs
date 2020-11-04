using GraphLib.GraphField;
using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex.Interface;
using System.Linq;
using System.Windows.Media.Media3D;

namespace Wpf3dVersion.Model
{
    public class Wpf3dGraphField : ModelVisual3D, IGraphField
    {
        public double DistanceBetween { get; set; }

        public Wpf3dGraphField()
        {
            DistanceBetween = 2;
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
                OffsetX = coordinates.First() * vertex.Size + 
                    coordinates.First() * DistanceBetween + offsetCorrection,
                OffsetY = coordinates.ElementAt(1) * vertex.Size + 
                    coordinates.ElementAt(1) * DistanceBetween + offsetCorrection,
                OffsetZ = coordinates.Last() * vertex.Size + 
                    coordinates.Last() * DistanceBetween + offsetCorrection
            };
            vertex.Transform = translate;
        }
    }
}
