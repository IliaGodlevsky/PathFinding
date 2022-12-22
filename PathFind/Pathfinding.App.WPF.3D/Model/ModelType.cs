using Shared.Extensions;
using System.Runtime.CompilerServices;
using System.Windows.Media.Media3D;

namespace Pathfinding.App.WPF._3D.Model
{
    internal abstract class ModelType
    {
        public static readonly ModelType Triangle = new TriangleModel();
        public static readonly ModelType Rectangle = new RectangleModel();

        protected abstract int[] TriangleIndices { get; }

        public Model3D Create(ITuple tuple, Material material)
        {
            var mesh = new MeshGeometry3D();
            mesh.Positions.AddRange(tuple.Enumerate<Point3D>());
            mesh.TriangleIndices.AddRange(TriangleIndices);
            return new GeometryModel3D(mesh, material) { BackMaterial = material };
        }

        private sealed class TriangleModel : ModelType
        {
            protected override int[] TriangleIndices { get; } = new[] { 0, 1, 2 };
        }

        private sealed class RectangleModel : ModelType
        {
            protected override int[] TriangleIndices { get; } = new[] { 0, 1, 2, 0, 2, 3 };
        }
    }
}
