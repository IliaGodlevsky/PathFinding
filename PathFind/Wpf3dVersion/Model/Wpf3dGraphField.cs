using GraphLib.GraphField;
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

        public Wpf3dGraphField(int width, 
            int length, int height) : this()
        {
            Width = width;
            Length = length;
            Height = height;
        }

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

        public void SetDistanceBetweenVertices()
        {
            foreach (Wpf3dVertex vertex in Children)
            {
                SetVertexOffset(vertex);
            }
        }

        public void CenterGraph(params double[] additionalOffset)
        {
            var dimensionSizes = new int[] { Width, Length, Height };
            var distancesToCenter = new double[dimensionSizes.Length];
            
            foreach (Wpf3dVertex vertex in Children)
            {
                for (int i = 0; i < dimensionSizes.Length; i++)
                {
                    distancesToCenter[i] = VertexOffset.GetVertexDistanceToCenter(
                        vertex.Size, 
                        dimensionSizes.ElementAtOrDefault(i), 
                        additionalOffset.ElementAtOrDefault(i), 
                        DistanceBetween.ElementAtOrDefault(i));
                }

                SetVertexOffset(vertex, distancesToCenter);
            }
        }

        private void SetVertexOffset(Wpf3dVertex vertex, 
            params double[] additionalOffset)
        {            
            var coordinates = vertex.Position.Coordinates;

            var offsetActions = new Action<TranslateTransform3D, double>[]
            {
                (transform, offset) => { transform.OffsetX = offset; },
                (transform, offset) => { transform.OffsetY = offset; },
                (transform, offset) => { transform.OffsetZ = offset; }
            };

            var translate = new TranslateTransform3D();

            for (int i = 0; i < coordinates.Count(); i++)
            {
                var offset = VertexOffset.GetVertexOffset(
                    coordinates.ElementAtOrDefault(i), 
                    vertex.Size, 
                    DistanceBetween.ElementAtOrDefault(i), 
                    additionalOffset.ElementAtOrDefault(i));

                offsetActions[i].Invoke(translate, offset);
            }

            vertex.Transform = translate;
        }

        private int Width { get; set; }

        private int Length { get; set; }

        private int Height { get; set; }

        private double[] DistanceBetween => new double[] 
        { 
            DistanceBetweenAtXAxis, 
            DistanceBetweenAtYAxis, 
            DistanceBetweenAtZAxis 
        };
    }

    static class VertexOffset
    {
        public static double GetVertexDistanceToCenter(double vertexSize,
            double dimensionSize, double additionalOffset, double distanceBetweenVertices)
        {
            return (-dimensionSize + additionalOffset) * (vertexSize + distanceBetweenVertices) / 2;
        }

        public static double GetVertexOffset(int coordinate,
            double size, double distanceBetween, double additionalOffset)
        {
            return coordinate * (size + distanceBetween) + additionalOffset;
        }
    }
}
