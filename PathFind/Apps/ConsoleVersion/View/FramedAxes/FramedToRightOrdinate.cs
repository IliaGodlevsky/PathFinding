using ConsoleVersion.View.Abstraction;
using ConsoleVersion.View.Interface;

namespace ConsoleVersion.View.FramedAxes
{
    internal sealed class FramedToRightOrdinate : FramedOrdinate, IFramedAxis, IDisplayable
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
            return string.Concat(Offset, VerticalFrameComponent, paddedCoordinate);
        }

        protected override string Offset => new string(Space, OffsetNumber);

        private int WidthOfOrdinateView => MainView.WidthOfOrdinateView;
        private int OffsetNumber => graphWidth * LateralDistance + WidthOfOrdinateView;

        private readonly int graphWidth;
    }
}
