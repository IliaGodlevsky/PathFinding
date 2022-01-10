using System.Windows.Media.Media3D;
using WPFVersion3D.Model;

namespace WPFVersion3D.Axes
{
    internal sealed class Ordinate : IAxis
    {
        public int Order => Constants.Ordinate;

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
