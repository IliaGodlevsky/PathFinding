using ConsoleVersion.Interface;

namespace ConsoleVersion.Model.FramedAxes
{
    internal sealed class FramedOverAbscissa : FramedAbscissa, IFramedAxis, IDisplayable
    {
        public FramedOverAbscissa(int graphWidth,
            int graphLength) : base(graphWidth)
        {
            this.graphLength = graphLength;
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
