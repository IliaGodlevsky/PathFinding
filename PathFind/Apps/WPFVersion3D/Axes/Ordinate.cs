using Common.Attrbiutes;
using System.Windows.Media.Media3D;
using WPFVersion3D.Model;

namespace WPFVersion3D.Axes
{
    [Order(1)]
    internal sealed class Ordinate : IAxis
    {
        public void Offset(TranslateTransform3D transfrom, double offset)
        {
            transfrom.OffsetY = offset;
        }

        public void SetDistanceBeetween(double distance, GraphField3D field)
        {
            field.DistanceBetweenVerticesAtYAxis = distance;
        }
    }
}
