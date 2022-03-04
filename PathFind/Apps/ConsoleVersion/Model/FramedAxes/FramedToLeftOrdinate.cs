using Common.Attrbiutes;
using GraphLib.Realizations.Graphs;

namespace ConsoleVersion.Model.FramedAxes
{
    [AttachedTo(typeof(GraphField)), Order(3)]
    internal sealed class FramedToLeftOrdinate : FramedOrdinate
    {
        public FramedToLeftOrdinate(Graph2D graph)
            : base(graph.Length)
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
