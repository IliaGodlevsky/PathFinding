using GraphLib.Realizations.Graphs;

namespace ConsoleVersion.Model.FramedAxes
{
    internal sealed class FramedOverAbscissa : FramedAbscissa
    {
        private readonly int graphLength;

        protected override string Offset { get; }

        public FramedOverAbscissa(Graph2D graph)
            : base(graph.Width)
        {
            this.graphLength = graph.Length;
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
