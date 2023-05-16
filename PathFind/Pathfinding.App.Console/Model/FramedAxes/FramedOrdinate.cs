using System.Text;

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
            yCoordinatePadding = AppLayout.YCoordinatePadding;
        }

        public override void Display()
        {
            System.Console.SetCursorPosition(0, AppLayout.HeightOfAbscissaView + 1);
            System.Console.Write(GetFramedAxis());
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