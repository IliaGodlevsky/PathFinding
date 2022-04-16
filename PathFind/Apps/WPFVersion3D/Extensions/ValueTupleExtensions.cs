using Common.Extensions.EnumerableExtensions;
using System.Windows.Media.Media3D;

namespace WPFVersion3D.Extensions
{
    internal static class ValueTupleExtensions
    {
        public static Model3D CreateRectangleModel(this (Point3D p0, Point3D p1, Point3D p2, Point3D p3) points, Material material)
        {
            var mesh = new MeshGeometry3D();

            mesh.Positions.AddRange(points.p0, points.p1, points.p2, points.p3);
            mesh.TriangleIndices.AddRange(0, 1, 2, 0, 2, 3);

            return new GeometryModel3D(mesh, material) { BackMaterial = material };
        }

        public static Model3D CreateTriangleModel(this (Point3D p0, Point3D p1, Point3D p2) points, Material material)
        {
            var mesh = new MeshGeometry3D();

            mesh.Positions.AddRange(points.p0, points.p1, points.p2);
            mesh.TriangleIndices.AddRange(0, 1, 2);

            return new GeometryModel3D(mesh, material) { BackMaterial = material };
        }
    }
}
