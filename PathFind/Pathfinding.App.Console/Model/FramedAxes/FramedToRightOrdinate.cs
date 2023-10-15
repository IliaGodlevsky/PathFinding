using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;

namespace Pathfinding.App.Console.Model.FramedAxes
{
    internal sealed class FramedToRightOrdinate : FramedOrdinate
    {
        private readonly int graphWidth;

        protected override string Offset => new string(Space, OffsetNumber);

        private int OffsetNumber => graphWidth * LateralDistance + AppLayout.WidthOfOrdinateView;

        public FramedToRightOrdinate(IGraph<Vertex> graph)
            : base(graph.GetLength())
        {
            graphWidth = graph.GetWidth();
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