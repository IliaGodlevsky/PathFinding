using System.Windows.Media.Media3D;
using WPFVersion3D.Model;

namespace WPFVersion3D.Axes
{
    /// <summary>
    /// Interface for 3D cartesian axis
    /// </summary>
    internal interface IAxis
    {
        /// <summary>
        /// Coordinate axis ordinal
        /// </summary>
        int Index { get; }

        void SetDistanceBeetween(double distance, GraphField3D field);

        void Offset(TranslateTransform3D transfrom, double offset);
    }
}
