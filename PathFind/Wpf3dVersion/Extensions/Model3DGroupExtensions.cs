using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Wpf3dVersion.Extensions
{
    public static class Model3DGroupExtensions
    {
        internal static void ChangeBrush(this Model3DGroup self, SolidColorBrush brush)
        {
            foreach (var children in self.Children)
            {
                ((children as GeometryModel3D).Material as DiffuseMaterial).Brush = brush;
            }
        }
    }
}
