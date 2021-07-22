using ConsoleVersion.View.Abstraction;

namespace ConsoleVersion.View.FramedAxes
{
    internal sealed class FramedUnderAbscissa : FramedAbscissa
    {
        public FramedUnderAbscissa(int graphWidth)
            : base(graphWidth)
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
