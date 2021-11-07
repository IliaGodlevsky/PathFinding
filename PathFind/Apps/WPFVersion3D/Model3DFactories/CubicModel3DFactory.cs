using Common.Extensions.EnumerableExtensions;
using System.Windows.Media.Media3D;
using WPFVersion3D.Interface;
using WPFVersion3D.Model;

namespace WPFVersion3D.Model3DFactories
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
        /// <param name="material">A material 
        /// from cube will consists of</param>
        /// <param name="cubeEdgeSize">A size of an
        /// edge of the cube</param>
        /// <returns>A cubic <see cref="Model3D"/> 
        /// with size <paramref name="cubeEdgeSize"/></returns>
        public Model3D CreateModel3D(double cubeEdgeSize, Material material)
        {
            var cube = new Model3DGroup();
            var points = GetPoints(cubeEdgeSize);
            var rectangles = GetRectangleModels3D(points, material);
            cube.Children.AddRange(rectangles);
            return cube;
        }

        private Point3D[] GetPoints(double edgeSize)
        {
            return new Point3D[]
            {
                new Point3D(0, 0, 0),
                new Point3D(edgeSize, 0, 0),
                new Point3D(edgeSize, 0, edgeSize),
                new Point3D(0, 0, edgeSize),
                new Point3D(0, edgeSize, edgeSize),
                new Point3D(edgeSize, edgeSize, edgeSize),
                new Point3D(edgeSize, edgeSize, 0),
                new Point3D(0, edgeSize, 0)
            };
        }

        private Model3D[] GetRectangleModels3D(Point3D[] points, Material material)
        {
            var reactangleFactory = new RectangleModel3DFactory();

            return new Model3D[]
            {
                reactangleFactory.CreateRectangleModel(points[4], points[3], points[2], points[5], material),
                reactangleFactory.CreateRectangleModel(points[5], points[2], points[1], points[6], material),
                reactangleFactory.CreateRectangleModel(points[7], points[6], points[1], points[0], material),
                reactangleFactory.CreateRectangleModel(points[7], points[0], points[3], points[4], material),
                reactangleFactory.CreateRectangleModel(points[7], points[4], points[5], points[6], material),
                reactangleFactory.CreateRectangleModel(points[0], points[1], points[2], points[3], material)
            };
        }
    }
}
