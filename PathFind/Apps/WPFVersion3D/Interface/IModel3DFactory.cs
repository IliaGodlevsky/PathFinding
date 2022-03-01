using System.Windows.Media.Media3D;

namespace WPFVersion3D.Interface
{
    internal interface IModel3DFactory
    {
        Model3D CreateModel3D(double size, Material material);
    }
}