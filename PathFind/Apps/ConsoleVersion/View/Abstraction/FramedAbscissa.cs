using Common.Extensions;
using System;
using System.Text;

namespace ConsoleVersion.View.Abstraction
{
    internal abstract class FramedAbscissa : FramedAxis
    {
        protected FramedAbscissa(int graphWidth) : base()
        {
            this.graphWidth = graphWidth;
        }

        protected string GetAbscissa()
        {
            return new StringBuilder(LargeSpace)
                .AppendRepeat(GetStringToAppend, graphWidth)
                .Append(LargeSpace)
                .ToString();
        }

        protected string GetHorizontalFrame()
        {
            return new StringBuilder(LargeSpace)
                .AppendRepeat(HorizontalFrameComponent, graphWidth)
                .Append(CoordinateDelimiter)
                .ToString();
        }

        private string GetStringToAppend(int index)
        {
            return string.Join(string.Empty, index, GetSpace(index));
        }

        private string LargeSpace => new string(Space, MainView.WidthOfOrdinateView);
        private string HorizontalFrameComponent
        {
            get
            {
                var frameComponent = new string(FrameComponent, LateralDistance - 1);
                return string.Join(string.Empty, CoordinateDelimiter, frameComponent);
            }
        }

        private string GetSpace(int index)
        {
            int indexLog = index.ToString().Length;
            int count = Math.Abs(LateralDistance - indexLog);
            return new string(Space, count);
        }

        protected const char Endl = '\n';
        protected const string NewLine = "\n";
        private const string CoordinateDelimiter = "+";
        private const char FrameComponent = '-';

        private readonly int graphWidth;
    }
}
