using Pathfinding.GraphLib.Core.Realizations.Graphs;

namespace Pathfinding.App.WPF._3D.Model.Axes
{
    internal sealed class Abscissa : Axis
    {
        protected override int Order => 2;

        public Abscissa(Graph3D<Vertex3D> graph) : base(graph)
        {

        }

        protected override void Offset(Vertex3D vertex, double offset)
        {
            vertex.FieldPosition.OffsetX = offset;
        }
    }
}
