using Pathfinding.App.WPF._3D.Extensions;
using Pathfinding.App.WPF._3D.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace Pathfinding.App.WPF._3D.Model3DFactories
{
    internal sealed class ConicModel3DFactory : IModel3DFactory
    {
        private const int Segments = 25;
        private const int Positions = 4;

        private const double Height = 5;
        private const double HalfHeight = Height / 2;

        private const double PI = 180;
        private const double PI2 = 360;

        public Model3D CreateModel3D(double diametre, Material material)
        {
            double radius = diametre / 2;
            var center = new Vector3D(radius, radius, radius);
            var points = GetPositions(0, radius, center);
            var rectangles = GetTriangleModels(points, material);
            var cone = new Model3DGroup();
            cone.Children.AddRange(rectangles);
            return cone;
        }

        private Point3D[,] GetPositions(double rtop, double rbottom, Vector3D center)
        {
            var points = new Point3D[Segments + 1, Positions];
            for (int i = 0; i < Segments + 1; i++)
            {
                points[i, 0] = GetPosition(rtop, i * PI2 / (Segments - 1), HalfHeight, center);
                points[i, 1] = GetPosition(rbottom, i * PI2 / (Segments - 1), -HalfHeight, center);
                points[i, 2] = GetPosition(0, i * PI2 / (Segments - 1), -HalfHeight, center);
                points[i, 3] = GetPosition(0, i * PI2 / (Segments - 1), HalfHeight, center);
            }
            return points;
        }

        private IEnumerable<Model3D> GetTriangleModels(Point3D[,] points, Material material)
        {
            for (int i = 0; i < Segments; i++)
            {
                var p0 = points[i, 0];
                var p1 = points[i, 1];
                var p2 = points[i, 2];
                var p3 = points[i, 3];
                var p4 = points[i + 1, 0];
                var p5 = points[i + 1, 1];

                yield return (p0, p4, p3).CreateTriangleModel(material);
                yield return (p1, p5, p2).CreateTriangleModel(material);
                yield return (p0, p1, p5).CreateTriangleModel(material);
                yield return (p0, p5, p4).CreateTriangleModel(material);
            }
        }

        private Point3D GetPosition(double radius,
            double theta, double y, Vector3D center)
        {
            double sn = Math.Sin(theta * Math.PI / 180);
            double cn = Math.Cos(theta * Math.PI / 180);
            double x = radius * cn;
            double z = -radius * sn;
            return new Point3D(x, y, z) + center;
        }
    }
}
