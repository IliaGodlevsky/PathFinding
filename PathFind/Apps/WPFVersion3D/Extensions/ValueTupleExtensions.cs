using Common.Extensions.EnumerableExtensions;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Media.Media3D;

namespace WPFVersion3D.Extensions
{
    internal static class ValueTupleExtensions
    {
        public static Model3D CreateRectangleModel(this (Point3D p0, Point3D p1, Point3D p2, Point3D p3) points, Material material)
        {
            var mesh = new MeshGeometry3D();

            mesh.Positions.AddRange(points.Enumerate());
            mesh.TriangleIndices.AddRange(0, 1, 2, 0, 2, 3);

            return new GeometryModel3D(mesh, material) { BackMaterial = material };
        }

        public static Model3D CreateTriangleModel(this (Point3D p0, Point3D p1, Point3D p2) points, Material material)
        {
            var mesh = new MeshGeometry3D();

            mesh.Positions.AddRange(points.Enumerate());
            mesh.TriangleIndices.AddRange(0, 1, 2);

            return new GeometryModel3D(mesh, material) { BackMaterial = material };
        }

        private static IEnumerable<Point3D> Enumerate(this ITuple tuple)
        {
            for (int i = 0; i < tuple.Length; i++)
            {
                yield return (Point3D)tuple[i];
            }
        }
    }
}
