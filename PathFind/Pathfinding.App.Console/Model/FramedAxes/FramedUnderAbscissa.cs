using Pathfinding.GraphLib.Core.Realizations.Graphs;

namespace Pathfinding.App.Console.Model.FramedAxes
{
    internal sealed class FramedUnderAbscissa : FramedAbscissa
    {
        protected override string Offset { get; }

        public FramedUnderAbscissa(Graph2D<Vertex> graph)
            : base(graph.Width)
        {
            Offset = string.Empty;
        }

        public override string GetFramedAxis()
        {
            string abscissa = GetAbscissa();
            string frame = GetHorizontalFrame();
            return string.Join(NewLine, abscissa, frame);
        }
    }
}