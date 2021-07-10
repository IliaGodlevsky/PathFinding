using Common.Extensions;
using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;
using WPFVersion3D.Interface;
using WPFVersion3D.Model;

namespace WPFVersion3D.Geomerties3D
{
    /// <summary>
    /// A class, that responds for creating 
    /// of spheric <see cref="Model3D"/>
    /// </summary>
    internal sealed class SphericModel3DFactory : IModel3DFactory
    {
        private const int Segments = 20;
        private const int Rings = 15;

        private const int PI = 180;
        private const int PI2 = 360;

        /// <summary>
        /// Creates a spheric <see cref="Model3D"/>
        /// </summary>
        /// <param name="diametre">A diametre of the sphere</param>
        /// <param name="material"></param>
        /// <returns></returns>
        public Model3D CreateModel3D(double diametre, Material material)
        {
            var sphere = new Model3DGroup();
            var points = GetPoints3D(diametre / 2);
            var squareModels = GetRectangleModels(points, material);
            sphere.Children.AddRange(squareModels);
            return sphere;
        }

        /// <summary>
        /// Returns all rectangle models, 
        /// that are needed to create sphere
        /// </summary>
        /// <param name="points"></param>
        /// <param name="material"></param>
        /// <returns></returns>
        private IEnumerable<Model3D> GetRectangleModels(Point3D[,] points, Material material)
        {
            var reactangleFactory = new RectangleModel3DFactory();

            for (int segment = 0; segment < Segments - 1; segment++)
            {
                for (int ring = 0; ring < Rings - 1; ring++)
                {
                    var p0 = points[segment, ring];
                    var p1 = points[segment + 1, ring];
                    var p2 = points[segment + 1, ring + 1];
                    var p3 = points[segment, ring + 1];
                    yield return reactangleFactory
                        .CreateRectangleModel(p0, p1, p2, p3, material);
                }
            }
        }

        /// <summary>
        /// Creates an array of points 
        /// for creation sphere segments
        /// </summary>
        /// <param name="radius"></param>
        /// <returns>An array of points 
        /// for sphere segments</returns>
        private Point3D[,] GetPoints3D(double radius)
        {
            var points = new Point3D[Segments, Rings];
            for (int segment = 0; segment < Segments; segment++)
            {
                for (int ring = 0; ring < Rings; ring++)
                {
                    double theta = segment * PI / (Segments - 1);
                    double phi = ring * PI2 / (Rings - 1);
                    points[segment, ring] = GetPosition(radius, theta, phi);
                }
            }

            return points;
        }

        /// <summary>
        /// Returns a point of a sphere segment
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="theta"></param>
        /// <param name="phi"></param>
        /// <returns>A point of the sphere segment</returns>
        private Point3D GetPosition(double radius, double theta, double phi)
        {
            double snt = Math.Sin(theta * Math.PI / PI);
            double cnt = Math.Cos(theta * Math.PI / PI);
            double snp = Math.Sin(phi * Math.PI / PI);
            double cnp = Math.Cos(phi * Math.PI / PI);

            double x = radius * (snt * cnp + 1);
            double y = radius * (cnt + 1);
            double z = radius * (-snt * snp + 1);

            return new Point3D(x, y, z);
        }
    }
}
