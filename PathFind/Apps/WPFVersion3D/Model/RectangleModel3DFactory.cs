using Common.Extensions.EnumerableExtensions;
using System.Windows.Media.Media3D;

namespace WPFVersion3D.Model
{
    internal sealed class RectangleModel3DFactory
    {
        public Model3D CreateRectangleModel(Point3D p0, Point3D p1,
            Point3D p2, Point3D p3, Material material)
        {
            var mesh = new MeshGeometry3D();

            mesh.Positions.AddRange(p0, p1, p2, p3);
            mesh.TriangleIndices.AddRange(0, 1, 2, 0, 2, 3);

            return new GeometryModel3D(mesh, material);
        }
    }
}
