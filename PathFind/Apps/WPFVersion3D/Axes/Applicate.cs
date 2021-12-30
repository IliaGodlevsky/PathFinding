using Common.Attrbiutes;
using System.Windows.Media.Media3D;
using WPFVersion3D.Model;

namespace WPFVersion3D.Axes
{
    [Order(0)]
    internal sealed class Applicate : IAxis
    {
        public void Offset(TranslateTransform3D transfrom, double offset)
        {
            transfrom.OffsetZ = offset;
        }

        public void SetDistanceBeetween(double distance, GraphField3D field)
        {
            field.DistanceBetweenVerticesAtZAxis = distance;
        }
    }
}
