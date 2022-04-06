using Common.Extensions.EnumerableExtensions;
using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;
using WPFVersion3D.Extensions;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Model3DFactories
{
    internal sealed class SphericModel3DFactory : IModel3DFactory
    {
        private const int Meridians = 15;
        private const int Latitudes = 20;

        private const int PI = 180;
        private const int PI2 = 360;

        public Model3D CreateModel3D(double diametre, Material material)
        {
            var sphere = new Model3DGroup();
            var points = GetPoints3D(diametre / 2);
            var rectangles = GetRectangleModels(points, material);
            sphere.Children.AddRange(rectangles);
            return sphere;
        }

        private IEnumerable<Model3D> GetRectangleModels(Point3D[,] points, Material material)
        {
            for (int latitude = 0; latitude < Latitudes - 1; latitude++)
            {
                for (int meridian = 0; meridian < Meridians - 1; meridian++)
                {
                    var p0 = points[latitude, meridian];
                    var p1 = points[latitude + 1, meridian];
                    var p2 = points[latitude + 1, meridian + 1];
                    var p3 = points[latitude, meridian + 1];
                    yield return (p0, p1, p2, p3).CreateRectangleModel(material);
                }
            }
        }

        private Point3D[,] GetPoints3D(double radius)
        {
            var points = new Point3D[Latitudes, Meridians];
            for (int latitude = 0; latitude < Latitudes; latitude++)
            {
                double theta = latitude * PI / (Latitudes - 1);
                for (int meridian = 0; meridian < Meridians; meridian++)
                {
                    double phi = meridian * PI2 / (Meridians - 1);
                    var position = GetPosition(radius, theta, phi);
                    points[latitude, meridian] = position;
                }
            }

            return points;
        }

        private Point3D GetPosition(double radius, double theta, double phi)
        {
            double snt = Math.Sin(theta * Math.PI / PI);
            double cnt = Math.Cos(theta * Math.PI / PI);
            double snp = Math.Sin(phi * Math.PI / PI);
            double cnp = Math.Cos(phi * Math.PI / PI);

            double x = radius * (snt * cnp + 1);
            double y = radius * (cnt + 1);
            double z = radius * (1 - snt * snp);

            return new Point3D(x, y, z);
        }
    }
}
