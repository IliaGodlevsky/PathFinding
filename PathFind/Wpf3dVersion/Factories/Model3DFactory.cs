using System.Windows.Media.Media3D;

namespace Wpf3dVersion.Factories
{
    internal static class Model3DFactory
    {
        internal static Model3DGroup GetCubicModel3D(double size, Material material)
        {
            var model = new Model3DGroup();

            var p0 = new Point3D(0d, 0d, 0d);
            var p1 = new Point3D(size, 0d, 0d);
            var p2 = new Point3D(size, 0d, size);
            var p3 = new Point3D(0d, 0d, size);
            var p4 = new Point3D(0d, size, size);
            var p5 = new Point3D(size, size, size);
            var p6 = new Point3D(size, size, 0d);
            var p7 = new Point3D(0d, size, 0d);

            model.Children.Add(CreateRectangleModel(p4, p3, p2, p5, material));
            model.Children.Add(CreateRectangleModel(p5, p2, p1, p6, material));
            model.Children.Add(CreateRectangleModel(p7, p6, p1, p0, material));
            model.Children.Add(CreateRectangleModel(p7, p0, p3, p4, material));
            model.Children.Add(CreateRectangleModel(p7, p4, p5, p6, material));
            model.Children.Add(CreateRectangleModel(p0, p1, p2, p3, material));

            return model;
        }

        private static MeshGeometry3D CreateRectangleMesh(Point3D p0, Point3D p1, Point3D p2, Point3D p3)
        {
            var mesh = new MeshGeometry3D();

            mesh.Positions.Add(p0);
            mesh.Positions.Add(p1);
            mesh.Positions.Add(p2);
            mesh.Positions.Add(p3);

            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);

            return mesh;
        }

        private static GeometryModel3D CreateRectangleModel(Point3D p0, Point3D p1, 
            Point3D p2, Point3D p3, Material material)
        {
            return new GeometryModel3D(CreateRectangleMesh(p0, p1, p2, p3), material);
        }

        
    }
}
