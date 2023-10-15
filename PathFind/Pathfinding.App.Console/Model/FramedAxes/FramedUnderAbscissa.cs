using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;

namespace Pathfinding.App.Console.Model.FramedAxes
{
    internal sealed class FramedUnderAbscissa : FramedAbscissa
    {
        protected override string Offset { get; }

        public FramedUnderAbscissa(IGraph<Vertex> graph)
            : base(graph.GetWidth())
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