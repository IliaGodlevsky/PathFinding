using Pathfinding.App.WPF._3D.Extensions;
using Pathfinding.App.WPF._3D.Interface;
using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace Pathfinding.App.WPF._3D.Model3DFactories
{
    internal sealed class ToricalModel3DFactory : IModel3DFactory
    {
        private const int Latitudes = 20;
        private const int Meridians = 15;

        private const double PI = 180;
        private const double PI2 = 360;

        public Model3D CreateModel3D(double diametre, Material material)
        {
            var points = GetPoints3D(diametre / 2);
            var rectangles = GetRectangleModels(points, material);
            return new Model3DGroup() { Children = new(rectangles) };
        }

        private Point3D GetPosition(double bigRadius,
            double smallRadius, double theta, double phi)
        {
            double snu = Math.Sin(theta * Math.PI / PI);
            double cnu = Math.Cos(theta * Math.PI / PI);
            double snv = Math.Sin(phi * Math.PI / PI);
            double cnv = Math.Cos(phi * Math.PI / PI);

            double x = (bigRadius + smallRadius * cnv) * cnu + bigRadius;
            double y = smallRadius * snv + bigRadius;
            double z = bigRadius - (bigRadius + smallRadius * cnv) * snu;

            return new(x, y, z);
        }

        private Point3D[,] GetPoints3D(double bigRadius)
        {
            double smallRadius = bigRadius / 2;
            var points = new Point3D[Latitudes, Meridians];
            for (int latitude = 0; latitude < Latitudes; latitude++)
            {
                double theta = latitude * PI2 / (Latitudes - 1);
                for (int meridian = 0; meridian < Meridians; meridian++)
                {
                    double phi = meridian * PI2 / (Meridians - 1);
                    var position = GetPosition(bigRadius, smallRadius, theta, phi);
                    points[latitude, meridian] = position;
                }
            }
            return points;
        }

        private IEnumerable<GeometryModel3D> GetRectangleModels(Point3D[,] points, Material material)
        {
            for (int latitude = 0; latitude < Latitudes - 1; latitude++)
            {
                for (int meridian = 0; meridian < Meridians - 1; meridian++)
                {
                    var p0 = points[latitude, meridian];
                    var p1 = points[latitude + 1, meridian];
                    var p2 = points[latitude + 1, meridian + 1];
                    var p3 = points[latitude, meridian + 1];
                    yield return (p0, p1, p2, p3).CreateRectangleGeometry(material);
                }
            }
        }
    }
}
