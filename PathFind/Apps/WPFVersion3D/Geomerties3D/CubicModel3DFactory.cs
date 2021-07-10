using Common.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Media3D;
using WPFVersion3D.Interface;
using WPFVersion3D.Model;

namespace WPFVersion3D.Geomerties3D
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
            var points = GetPoints(cubeEdgeSize).ToArray();
            var models = GetRectangleModels3D(points, material);
            cube.Children.AddRange(models);
            return cube;
        }

        public IEnumerable<Point3D> GetPoints(double cubeEdgeSize)
        {
            yield return new Point3D(0, 0, 0);
            yield return new Point3D(cubeEdgeSize, 0, 0);
            yield return new Point3D(cubeEdgeSize, 0, cubeEdgeSize);
            yield return new Point3D(0, 0, cubeEdgeSize);
            yield return new Point3D(0, cubeEdgeSize, cubeEdgeSize);
            yield return new Point3D(cubeEdgeSize, cubeEdgeSize, cubeEdgeSize);
            yield return new Point3D(cubeEdgeSize, cubeEdgeSize, 0);
            yield return new Point3D(0, cubeEdgeSize, 0);
        }

        private IEnumerable<Model3D> GetRectangleModels3D(Point3D[] points, Material material)
        {
            var reactangleFactory = new RectangleModel3DFactory();

            yield return reactangleFactory.CreateRectangleModel(points[4], points[3], points[2], points[5], material);
            yield return reactangleFactory.CreateRectangleModel(points[5], points[2], points[1], points[6], material);
            yield return reactangleFactory.CreateRectangleModel(points[7], points[6], points[1], points[0], material);
            yield return reactangleFactory.CreateRectangleModel(points[7], points[0], points[3], points[4], material);
            yield return reactangleFactory.CreateRectangleModel(points[7], points[4], points[5], points[6], material);
            yield return reactangleFactory.CreateRectangleModel(points[0], points[1], points[2], points[3], material);
        }
    }
}
