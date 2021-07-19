using Common.Extensions;
using ConsoleVersion.View.Abstraction;
using System.Text;

namespace ConsoleVersion.View.FramedAxes
{
    internal sealed class FramedToLeftOrdinate : FramedOrdinate
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
            return string.Join(string.Empty, paddedCoordinate, VerticalFrameComponent);
        }

        protected override string GetOffset()
        {
            return string.Empty;
        }
    }
}
