using Colorful;
using Common.Extensions;
using System.Text;

namespace ConsoleVersion.Model.FramedAxes
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
            yCoordinatePadding = Constants.YCoordinatePadding;
        }

        public override void Display()
        {
            Console.SetCursorPosition(0, Constants.HeightOfAbscissaView + 1);
            Console.Write(GetFramedAxis());
        }

        public override string GetFramedAxis()
        {
            var stringBuilder = new StringBuilder();
            return stringBuilder.AppendLineMany(GetStringToAppend, graphLength).ToString();
        }

        protected abstract string GetStringToAppend(int yCoordinate);

        protected abstract string GetPaddedYCoordinate(int yCoordinate);
    }
}