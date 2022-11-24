using System.Windows.Media.Media3D;

namespace Pathfinding.App.WPF._3D.Interface
{
    public interface IModel3DFactory
    {
        Model3D CreateModel3D(double size, Material material);
    }
}