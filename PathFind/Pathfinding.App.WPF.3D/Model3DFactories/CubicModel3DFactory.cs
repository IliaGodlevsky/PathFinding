using Pathfinding.App.WPF._3D.Extensions;
using Pathfinding.App.WPF._3D.Interface;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace Pathfinding.App.WPF._3D.Model3DFactories
{
    internal sealed class CubicModel3DFactory : IModel3DFactory
    {
        public Model3D CreateModel3D(double cubeEdgeSize, Material material)
        {
            var points = GetPoints(cubeEdgeSize);
            var rectangles = GetRectangleModels3D(points, material);
            return new Model3DGroup() { Children = new(rectangles) };
        }

        private Point3D[] GetPoints(double edgeSize)
        {
            return new Point3D[]
            {
                new (0, 0, 0),
                new (edgeSize, 0, 0),
                new (edgeSize, 0, edgeSize),
                new (0, 0, edgeSize),
                new (0, edgeSize, edgeSize),
                new (edgeSize, edgeSize, edgeSize),
                new (edgeSize, edgeSize, 0),
                new (0, edgeSize, 0)
            };
        }

        private IEnumerable<GeometryModel3D> GetRectangleModels3D(Point3D[] points, Material material)
        {
            yield return (points[4], points[3], points[2], points[5]).CreateRectangleGeometry(material);
            yield return (points[5], points[2], points[1], points[6]).CreateRectangleGeometry(material);
            yield return (points[7], points[6], points[1], points[0]).CreateRectangleGeometry(material);
            yield return (points[7], points[0], points[3], points[4]).CreateRectangleGeometry(material);
            yield return (points[7], points[4], points[5], points[6]).CreateRectangleGeometry(material);
            yield return (points[0], points[1], points[2], points[3]).CreateRectangleGeometry(material);
        }
    }
}
