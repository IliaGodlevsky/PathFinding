using System.Windows.Media.Media3D;
using WPFVersion3D.Model;

namespace WPFVersion3D.Axes
{
    internal interface IAxis
    {
        int IndexNumber { get; }

        void SetDistanceBeetween(double distance, GraphField3D field);

        void Offset(TranslateTransform3D transfrom, double offset);
    }
}
