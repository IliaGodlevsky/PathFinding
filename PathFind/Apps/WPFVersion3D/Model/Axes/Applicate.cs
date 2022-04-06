using System.Collections.Generic;

namespace WPFVersion3D.Model.Axes
{
    internal sealed class Applicate : Axis
    {
        protected override int Order => 0;

        public Applicate(int[] dimensionSizes, IReadOnlyCollection<Vertex3D> vertices)
            : base(dimensionSizes, vertices)
        {
        }

        protected override void Offset(Vertex3D vertex, double offset)
        {
            vertex.FieldPosition.OffsetZ = offset;
        }
    }
}