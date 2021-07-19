using Common.Extensions;
using ConsoleVersion.View.Abstraction;
using System.Text;

namespace ConsoleVersion.View.FramedAxes
{
    internal sealed class FramedToRightOrdinate : FramedOrdinate
    {
        public FramedToRightOrdinate(int graphWidth, int graphLength)
            : base(graphLength)
        {
            this.graphWidth = graphWidth;
        }

        protected override string GetPaddedYCoordinate(int yCoordinate)
        {
            return yCoordinate.ToString().PadRight(yCoordinatePadding);
        }

        protected override string GetStringToAppend(int yCoordinate)
        {
            string paddedCoordinate = GetPaddedYCoordinate(yCoordinate);
            return string.Join(string.Empty, GetOffset(),
                VerticalFrameComponent, paddedCoordinate);
        }

        protected override string GetOffset()
        {
            return new string(Space, OffsetNumber);
        }

        private int WidthOfOrdinateView => MainView.WidthOfOrdinateView;
        private int OffsetNumber => graphWidth * LateralDistance + WidthOfOrdinateView;

        private readonly int graphWidth;
    }
}
