using System.Windows.Media.Media3D;
using WPFVersion3D.Model;

namespace WPFVersion3D.Axes
{
    internal sealed class Abscissa : IAxis
    {
        public int Index => 2;

        public void Offset(TranslateTransform3D transfrom, double offset)
        {
            transfrom.OffsetX = offset;
        }

        public void SetDistanceBeetween(double distance, GraphField3D field)
        {
            field.DistanceBetweenVerticesAtXAxis = distance;
        }
    }
}
