using GraphLib.Realizations.Graphs;
using System.Collections.Generic;

namespace WPFVersion3D.Model.Axes
{
    internal sealed class Applicate : Axis
    {
        protected override int Order => 0;

        public Applicate(Graph3D graph) : base(graph)
        {
        }

        protected override void Offset(Vertex3D vertex, double offset)
        {
            vertex.FieldPosition.OffsetZ = offset;
        }
    }
}