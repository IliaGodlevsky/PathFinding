using ConsoleVersion.View.Abstraction;

namespace ConsoleVersion.View.FramedAxes
{
    internal sealed class FramedOverAbscissa : FramedAbscissa
    {
        public FramedOverAbscissa(int graphWidth,
            int graphLength) : base(graphWidth)
        {
            this.graphLength = graphLength;
        }

        protected override string GetOffset()
        {
            return new string(Endl, graphLength + 1);
        }

        public override string GetFramedAxis()
        {
            string frame = GetHorizontalFrame();
            string abscissa = GetAbscissa();
            return string.Join(NewLine, GetOffset(), frame, abscissa);
        }

        private readonly int graphLength;
    }
}
