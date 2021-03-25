using System.Windows.Media.Media3D;

namespace WPFVersion3D.Model
{
    /// <summary>
    /// Creates geometric 3D models
    /// </summary>
    internal static class Model3DFactory
    {
        /// <summary>
        /// Creates cubic 3D model of <paramref name="modelSize"/> 
        /// size and from <paramref name="modelMaterial"/> material
        /// </summary>
        /// <param name="modelSize"></param>
        /// <param name="modelMaterial"></param>
        /// <returns>A cubic 3D model with edge of size <paramref name="modelSize"/> 
        /// and from <paramref name="modelMaterial"/> material</returns>
        internal static Model3D CreateCubicModel3D(double modelSize, Material modelMaterial)
        {
            var model = new Model3DGroup();

            var p0 = new Point3D(0, 0, 0);
            var p1 = new Point3D(modelSize, 0, 0);
            var p2 = new Point3D(modelSize, 0, modelSize);
            var p3 = new Point3D(0, 0, modelSize);
            var p4 = new Point3D(0, modelSize, modelSize);
            var p5 = new Point3D(modelSize, modelSize, modelSize);
            var p6 = new Point3D(modelSize, modelSize, 0);
            var p7 = new Point3D(0, modelSize, 0);

            model.Children.Add(CreateRectangleModel(p4, p3, p2, p5, modelMaterial));
            model.Children.Add(CreateRectangleModel(p5, p2, p1, p6, modelMaterial));
            model.Children.Add(CreateRectangleModel(p7, p6, p1, p0, modelMaterial));
            model.Children.Add(CreateRectangleModel(p7, p0, p3, p4, modelMaterial));
            model.Children.Add(CreateRectangleModel(p7, p4, p5, p6, modelMaterial));
            model.Children.Add(CreateRectangleModel(p0, p1, p2, p3, modelMaterial));

            return model;
        }

        private static GeometryModel3D CreateRectangleModel(
            Point3D p0, Point3D p1,
            Point3D p2, Point3D p3,
            Material material)
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

            return new GeometryModel3D(mesh, material);
        }
    }
}
