using Colorful;
using Common.Extensions;
using ConsoleVersion.Interface;
using System.Text;

namespace ConsoleVersion.View.Abstraction
{
    internal abstract class FramedOrdinate : FramedAxis, IFramedAxis, IDisplayable
    {
        protected FramedOrdinate(int graphLength) : base()
        {
            this.graphLength = graphLength;
            yCoordinatePadding = MainView.YCoordinatePadding;
        }

        public override void Display()
        {
            Console.SetCursorPosition(0, MainView.HeightOfAbscissaView + 1);
            Console.Write(GetFramedAxis());
        }

        public override string GetFramedAxis()
        {
            var stringBuilder = new StringBuilder();
            return stringBuilder.AppendLineMany(GetStringToAppend, graphLength).ToString();
        }

        protected abstract string GetStringToAppend(int yCoordinate);

        protected abstract string GetPaddedYCoordinate(int yCoordinate);

        protected readonly int graphLength;
        protected readonly int yCoordinatePadding;

        protected const string VerticalFrameComponent = "|";
    }
}
