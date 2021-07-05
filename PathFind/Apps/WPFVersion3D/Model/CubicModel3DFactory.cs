using System.Windows.Media.Media3D;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Model
{
    /// <summary>
    /// A class that responds for 
    /// creating a cubic <see cref="Model3D"/>
    /// </summary>
    internal sealed class CubicModel3DFactory : IModel3DFactory
    {
        /// <summary>
        /// Creates cubic <see cref="Model3D"/>
        /// </summary>
        /// <param name="material">a material 
        /// from cube will consists of</param>
        /// <param name="modelSize">a size of all 
        /// edges of the cube</param>
        /// <returns>A cubic <see cref="Model3D"/> 
        /// with size <paramref name="modelSize"/></returns>
        public Model3D CreateModel3D(double modelSize, Material material)
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

            model.Children.Add(CreateRectangleModel(p4, p3, p2, p5, material));
            model.Children.Add(CreateRectangleModel(p5, p2, p1, p6, material));
            model.Children.Add(CreateRectangleModel(p7, p6, p1, p0, material));
            model.Children.Add(CreateRectangleModel(p7, p0, p3, p4, material));
            model.Children.Add(CreateRectangleModel(p7, p4, p5, p6, material));
            model.Children.Add(CreateRectangleModel(p0, p1, p2, p3, material));

            return model;
        }

        private GeometryModel3D CreateRectangleModel(
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
