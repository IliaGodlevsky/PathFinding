using Pathfinding.App.Console;
using Pathfinding.App.Console.Extensions;
using System;
using System.Text;

namespace Pathfinding.App.Console.Model.FramedAxes
{
    internal abstract class FramedAbscissa : FramedAxis
    {
        private const string CoordinateDelimiter = "+";
        private const char FrameComponent = '-';

        protected const char Endl = '\n';
        protected const string NewLine = "\n";

        private static readonly string LargeSpace = new string(Space, Screen.WidthOfOrdinateView);

        private readonly int graphWidth;

        protected FramedAbscissa(int graphWidth) : base()
        {
            this.graphWidth = graphWidth;
        }

        public override void Display()
        {
            System.Console.SetCursorPosition(0, Screen.HeightOfGraphParametresView);
            System.Console.Write(GetFramedAxis());
        }

        protected string GetAbscissa()
        {
            var stringBuilder = new StringBuilder(LargeSpace);
            for (int i = 0; i < graphWidth; i++)
            {
                string line = GetAbscissaFragment(i);
                stringBuilder.Append(line);
            }
            return stringBuilder.Append(LargeSpace).ToString();
        }

        protected string GetHorizontalFrame()
        {
            var stringBuilder = new StringBuilder(LargeSpace);
            string fragment = GetHorizontalFrameFragment();
            int times = graphWidth;
            while (times-- > 0)
            {
                stringBuilder.Append(fragment);
            }
            return stringBuilder.Append(CoordinateDelimiter).ToString();
        }

        private string GetAbscissaFragment(int index)
        {
            return string.Concat(index, GetSpace(index));
        }

        private string GetHorizontalFrameFragment()
        {
            var frameComponent = new string(FrameComponent, LateralDistance - 1);
            return string.Concat(CoordinateDelimiter, frameComponent);
        }

        private string GetSpace(int index)
        {
            int numberLength = index.GetDigitsNumber();
            int count = Math.Abs(LateralDistance - numberLength);
            return new string(Space, count);
        }
    }
}
