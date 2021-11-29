using ConsoleVersion.Interface;

namespace ConsoleVersion.Model.FramedAxes
{
    internal sealed class FramedToLeftOrdinate : FramedOrdinate, IFramedAxis, IDisplayable
    {
        public FramedToLeftOrdinate(int graphLength)
            : base(graphLength)
        {

        }

        protected override string GetPaddedYCoordinate(int yCoordinate)
        {
            return yCoordinate.ToString().PadLeft(yCoordinatePadding);
        }

        protected override string GetStringToAppend(int yCoordinate)
        {
            string paddedCoordinate = GetPaddedYCoordinate(yCoordinate);
            return string.Concat(paddedCoordinate, VerticalFrameComponent);
        }

        protected override string Offset => string.Empty;
    }
}
