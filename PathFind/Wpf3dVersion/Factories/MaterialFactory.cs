using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Wpf3dVersion.Factories
{
    internal static class MaterialFactory
    {
        internal static DiffuseMaterial GetDiffuseMaterial(Color color, double opacity)
        {
            var diffuseMaterial = new DiffuseMaterial
            {
                Brush = new SolidColorBrush(color)
            };

            diffuseMaterial.Brush.Opacity = opacity;
            return diffuseMaterial;
        }
    }
}
