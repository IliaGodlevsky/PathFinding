using Colorful;
using Pathfinding.App.Console;
using System.Text;

using ColorfulConsole = Colorful.Console;

namespace Pathfinding.App.Console.Model.FramedAxes
{
    internal abstract class FramedOrdinate : FramedAxis
    {
        protected const string VerticalFrameComponent = "|";

        protected readonly int graphLength;
        protected readonly int yCoordinatePadding;

        protected FramedOrdinate(int graphLength)
            : base()
        {
            this.graphLength = graphLength;
            yCoordinatePadding = Screen.YCoordinatePadding;
        }

        public override void Display()
        {
            ColorfulConsole.SetCursorPosition(0, Screen.HeightOfAbscissaView + 1);
            ColorfulConsole.Write(GetFramedAxis());
        }

        public override string GetFramedAxis()
        {
            var stringBuilder = new StringBuilder();
            for (int i = 0; i < graphLength; i++)
            {
                string line = GetStringToAppend(i);
                stringBuilder.AppendLine(line);
            }
            return stringBuilder.ToString();
        }

        protected abstract string GetStringToAppend(int yCoordinate);

        protected abstract string GetPaddedYCoordinate(int yCoordinate);
    }
}