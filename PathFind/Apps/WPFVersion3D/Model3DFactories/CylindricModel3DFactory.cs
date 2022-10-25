﻿using Common.Extensions.EnumerableExtensions;
using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;
using WPFVersion3D.Extensions;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Model3DFactories
{
    internal sealed class CylindricModel3DFactory : IModel3DFactory
    {
        private const int Segments = 30;
        private const double Height = 4;
        private const double HalfHeight = Height / 2;
        private const int Positions = 4;

        private const double PI = 180;
        private const double PI2 = 360;

        public Model3D CreateModel3D(double diametre, Material material)
        {
            double radius = diametre / 2;
            var center = new Vector3D(radius, radius, radius);
            var points = GetPoints3D(radius, center);
            var rectangles = GetTriangleModels(points, material);
            var cylinder = new Model3DGroup();
            cylinder.Children.AddRange(rectangles);
            return cylinder;
        }

        private IEnumerable<Model3D> GetTriangleModels(Point3D[,] points, Material material)
        {
            for (int i = 0; i < Segments - 1; i++)
            {
                var p0 = points[i, 0];
                var p1 = points[i, 1];
                var p2 = points[i, 2];
                var p3 = points[i, 3];
                var p4 = points[i + 1, 0];
                var p5 = points[i + 1, 1];
                var p6 = points[i + 1, 2];
                var p7 = points[i + 1, 3];

                yield return (p0, p4, p3).CreateTriangleModel(material);
                yield return (p4, p7, p3).CreateTriangleModel(material);
                yield return (p1, p5, p2).CreateTriangleModel(material);
                yield return (p5, p6, p2).CreateTriangleModel(material);
                yield return (p0, p1, p4).CreateTriangleModel(material);
                yield return (p1, p5, p4).CreateTriangleModel(material);
                yield return (p2, p7, p6).CreateTriangleModel(material);
                yield return (p2, p3, p7).CreateTriangleModel(material);
            }
        }

        private Point3D[,] GetPoints3D(double smallRadius, Vector3D center)
        {
            var points = new Point3D[Segments, Positions];
            for (int i = 0; i < Segments; i++)
            {
                points[i, 0] = GetPosition(0, i * PI2 / (Segments - 1), HalfHeight, center);
                points[i, 1] = GetPosition(0, i * PI2 / (Segments - 1), -HalfHeight, center);
                points[i, 2] = GetPosition(smallRadius, i * PI2 / (Segments - 1), -HalfHeight, center);
                points[i, 3] = GetPosition(smallRadius, i * PI2 / (Segments - 1), HalfHeight, center);
            }
            return points;
        }

        private Point3D GetPosition(double radius,
            double theta, double y, Vector3D center)
        {
            double sn = Math.Sin(theta * Math.PI / PI);
            double cn = Math.Cos(theta * Math.PI / PI);
            double x = radius * cn;
            double z = radius * sn;
            return new Point3D(x, y, z) + center;
        }
    }
}
