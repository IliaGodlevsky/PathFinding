using GraphLib.GraphField;
using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex.Interface;
using System;
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
            {
                SetVertexOffset(vertex);
            }
        }

        public void CenterGraph(IGraph graph, double additionalOffset = 0.0)
        {
            var dimensionSizes = graph.DimensionsSizes.ToArray();
            var distancesToCenter = new double[dimensionSizes.Length];
            
            foreach (Wpf3dVertex vertex in graph)
            {
                for (int i = 0; i < dimensionSizes.Length; i++)
                {
                    distancesToCenter[i] = GetVertexDistanceToCenter(vertex.Size, 
                        dimensionSizes[i], additionalOffset, DistanceBetween[i]);
                }

                SetVertexOffset(vertex, distancesToCenter);
            }
        }

        private void SetVertexOffset(Wpf3dVertex vertex, params double[] additionalOffset)
        {
            var coordinates = vertex.Position.Coordinates;
            if (coordinates.Count() != 3)
            {
                throw new ArgumentException("Must be 3D coordinates");
            }

            var translate = new TranslateTransform3D
            {
                OffsetX = coordinates.ElementAt(0) * (vertex.Size + DistanceBetweenAtXAxis) + additionalOffset.ElementAtOrDefault(0),
                OffsetY = coordinates.ElementAt(1) * (vertex.Size + DistanceBetweenAtYAxis) + additionalOffset.ElementAtOrDefault(1),
                OffsetZ = coordinates.ElementAt(2) * (vertex.Size + DistanceBetweenAtZAxis) + additionalOffset.ElementAtOrDefault(2)
            };

            vertex.Transform = translate;
        }

        private double[] DistanceBetween => new double[] 
        { 
            DistanceBetweenAtXAxis, 
            DistanceBetweenAtYAxis, 
            DistanceBetweenAtZAxis 
        };

        private static double GetVertexDistanceToCenter(double vertexSize, 
            double dimensionSize, double additionalOffset, double distanceBetweenVertices)
        {
            return (-dimensionSize + additionalOffset) * (vertexSize + distanceBetweenVertices) / 2;
        }
    }
}
