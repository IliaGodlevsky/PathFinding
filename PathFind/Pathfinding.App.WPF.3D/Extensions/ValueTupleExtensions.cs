using Shared.Extensions;
using System.Runtime.CompilerServices;
using System.Windows.Media.Media3D;

namespace Pathfinding.App.WPF._3D.Extensions
{
    internal static class ValueTupleExtensions
    {
        public static Model3D CreateRectangleModel(
            this (Point3D p0, Point3D p1, Point3D p2, Point3D p3) points, Material material)
        {
            return points.CreateModel(material, 0, 1, 2, 0, 2, 3);
        }

        public static Model3D CreateTriangleModel(
            this (Point3D p0, Point3D p1, Point3D p2) points, Material material)
        {
            return points.CreateModel(material, 0, 1, 2);
        }

        private static Model3D CreateModel(this ITuple tuple,
            Material material, params int[] triangleIndices)
        {
            var mesh = new MeshGeometry3D();
            mesh.Positions.AddRange(tuple.Enumerate<Point3D>());
            mesh.TriangleIndices.AddRange(triangleIndices);
            return new GeometryModel3D(mesh, material) { BackMaterial = material };
        }
    }
}
