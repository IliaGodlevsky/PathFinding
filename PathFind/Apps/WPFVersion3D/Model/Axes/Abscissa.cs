using System.Collections.Generic;

namespace WPFVersion3D.Model.Axes
{
    internal sealed class Abscissa : Axis
    {
        protected override int Order => 2;

        public Abscissa(int[] dimensionSizes, IReadOnlyCollection<Vertex3D> vertices)
            : base(dimensionSizes, vertices)
        {

        }

        protected override void Offset(Vertex3D vertex, double offset)
        {
            vertex.FieldPosition.OffsetX = offset;
        }
    }
}
