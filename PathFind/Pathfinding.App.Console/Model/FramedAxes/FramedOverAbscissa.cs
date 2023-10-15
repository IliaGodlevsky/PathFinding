using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;

namespace Pathfinding.App.Console.Model.FramedAxes
{
    internal sealed class FramedOverAbscissa : FramedAbscissa
    {
        private readonly int graphLength;

        protected override string Offset { get; }

        public FramedOverAbscissa(IGraph<Vertex> graph)
            : base(graph.GetWidth())
        {
            graphLength = graph.GetLength();
            Offset = new string(Endl, graphLength + 1);
        }

        public override string GetFramedAxis()
        {
            string frame = GetHorizontalFrame();
            string abscissa = GetAbscissa();
            return string.Join(NewLine, Offset, frame, abscissa);
        }
    }
}
