using Common.Attrbiutes;
using GraphLib.Realizations.Graphs;

namespace ConsoleVersion.Model.FramedAxes
{
    [AttachedTo(typeof(GraphField)), Order(1)]
    internal sealed class FramedUnderAbscissa : FramedAbscissa
    {
        protected override string Offset { get; }

        public FramedUnderAbscissa(Graph2D graph)
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