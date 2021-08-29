using System;
using System.Text;

using Console = Colorful.Console;

namespace ConsoleVersion.View.Abstraction
{
    internal abstract class FramedAbscissa : FramedAxis
    {
        protected FramedAbscissa(int graphWidth) : base()
        {
            this.graphWidth = graphWidth;
        }

        public override void Display()
        {
            Console.SetCursorPosition(0, MainView.HeightOfGraphParametresView);
            Console.Write(GetFramedAxis());
        }

        protected string GetAbscissa()
        {
            string largeSpace = LargeSpace;
            var stringBuilder = new StringBuilder(largeSpace);
            for (int index = 0; index < graphWidth; index++)
            {
                stringBuilder.Append(GetAbscissaFragment(index));
            }
            return stringBuilder.Append(largeSpace).ToString();
        }

        protected string GetHorizontalFrame()
        {
            var stringBuilder = new StringBuilder(LargeSpace);
            for (int index = 0; index < graphWidth; index++)
            {
                stringBuilder.Append(HorizontalFrameFragment);
            }
            return stringBuilder.Append(CoordinateDelimiter).ToString();
        }

        private string GetAbscissaFragment(int index)
        {
            return string.Concat(index, GetSpace(index));
        }

        private string LargeSpace => new string(Space, MainView.WidthOfOrdinateView);
        private string HorizontalFrameFragment
        {
            get
            {
                var frameComponent = new string(FrameComponent, LateralDistance - 1);
                return string.Concat(CoordinateDelimiter, frameComponent);
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
