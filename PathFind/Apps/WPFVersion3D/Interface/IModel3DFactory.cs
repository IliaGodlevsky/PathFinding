using Common.Interface;
using System.Windows.Media.Media3D;

namespace WPFVersion3D.Interface
{
    /// <summary>
    /// An interface that responds
    /// for creating a <see cref="Model3D"/> 
    /// of regular geometric figures
    /// </summary>
    internal interface IModel3DFactory : ICloneable<IModel3DFactory>
    {
        /// <summary>
        /// Creates a <see cref="Model3D"/> 
        /// of a regular geometric figure
        /// </summary>
        /// <param name="size">size of the <see cref="Model3D"/>
        /// (edge for cube or diametre for sphere)</param>
        /// <param name="material"><see cref="Material"/> of 
        /// the creatable <see cref="Model3D"/></param>
        /// <returns>A <see cref="Model3D"/> of a 
        /// regular geometric figure</returns>
        Model3D CreateModel3D(double size, Material material);
    }
}