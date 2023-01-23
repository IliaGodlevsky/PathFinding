using System.Windows.Media.Media3D;

namespace Pathfinding.App.WPF._3D.Extensions
{
    internal static class Point3DTupleExtensions
    {
        private static readonly int[] RectangleIndices = { 0, 1, 2, 0, 2, 3 };
        private static readonly int[] TriangleIndices = { 0, 1, 2 };

        public static GeometryModel3D CreateRectangleGeometry(
            this (Point3D p0, Point3D p1, Point3D p2, Point3D p3) points, Material material)
        {
            return CreateGeometry(material, RectangleIndices,
                points.p0, points.p1, points.p2, points.p3);
        }

        public static GeometryModel3D CreateTriangleGeometry(
            this (Point3D p0, Point3D p1, Point3D p2) points, Material material)
        {
            return CreateGeometry(material, TriangleIndices,
                points.p0, points.p1, points.p2);
        }

        private static GeometryModel3D CreateGeometry(Material material,
            int[] triangleIndices, params Point3D[] points)
        {
            return new()
            {
                Geometry = new MeshGeometry3D()
                {
                    TriangleIndices = new(triangleIndices),
                    Positions = new(points)
                },
                Material = material,
                BackMaterial = material
            };
        }
    }
}
