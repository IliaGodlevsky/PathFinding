using Common.Attrbiutes;
using GraphLib.Realizations.Graphs;

namespace ConsoleVersion.Model.FramedAxes
{
    [AttachedTo(typeof(GraphField)), Order(1)]
    internal sealed class FramedUnderAbscissa : FramedAbscissa
    {
        public FramedUnderAbscissa(Graph2D graph)
            : base(graph.Width)
        {

        }

        public override string GetFramedAxis()
        {
            string abscissa = GetAbscissa();
            string frame = GetHorizontalFrame();
            return string.Join(NewLine, abscissa, frame);
        }

        protected override string Offset => string.Empty;
    }
}
