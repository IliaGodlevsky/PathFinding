using System.Collections.Generic;

namespace WPFVersion3D.Model.Axes
{
    internal sealed class Ordinate : Axis
    {
        protected override int Order => 1;

        public Ordinate(int[] dimensionSizes, IReadOnlyCollection<Vertex3D> vertices)
            : base(dimensionSizes, vertices)
        {
        }

        protected override void Offset(Vertex3D vertex, double offset)
        {
            vertex.FieldPosition.OffsetY = offset;
        }
    }
}
