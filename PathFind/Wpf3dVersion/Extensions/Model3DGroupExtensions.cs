using System.Windows.Media.Media3D;

namespace Wpf3dVersion.Extensions
{
    public static class Model3DGroupExtensions
    {
        internal static void ChangeMaterial(this Model3DGroup self, Material material)
        {
            foreach(var children in self.Children)
            {
                (children as GeometryModel3D).Material = material;
            }
        }
    }
}
