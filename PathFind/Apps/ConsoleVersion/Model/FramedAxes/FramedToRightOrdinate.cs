using Common.Attrbiutes;
using GraphLib.Realizations.Graphs;

namespace ConsoleVersion.Model.FramedAxes
{
    [AttachedTo(typeof(GraphField)), Order(2)]
    internal sealed class FramedToRightOrdinate : FramedOrdinate
    {
        private readonly int graphWidth;

        protected override string Offset => new string(Space, OffsetNumber);

        private int OffsetNumber => graphWidth * LateralDistance + Constants.WidthOfOrdinateView;

        public FramedToRightOrdinate(Graph2D graph)
            : base(graph.Length)
        {
            this.graphWidth = graph.Width;
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
    }
}