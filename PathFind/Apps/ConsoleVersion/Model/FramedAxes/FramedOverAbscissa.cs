using Common.Attrbiutes;
using GraphLib.Realizations.Graphs;

namespace ConsoleVersion.Model.FramedAxes
{
    [AttachedTo(typeof(GraphField)), Order(0)]
    internal sealed class FramedOverAbscissa : FramedAbscissa
    {
        public FramedOverAbscissa(Graph2D graph) : base(graph.Width)
        {
            this.graphLength = graph.Length;
        }

        protected override string Offset => new string(Endl, graphLength + 1);

        public override string GetFramedAxis()
        {
            string frame = GetHorizontalFrame();
            string abscissa = GetAbscissa();
            return string.Join(NewLine, Offset, frame, abscissa);
        }

        private readonly int graphLength;
    }
}
