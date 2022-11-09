using Pathfinding.GraphLib.Core.Realizations.Graphs;

namespace Pathfinding.App.Console.Model.FramedAxes
{
    internal sealed class FramedToLeftOrdinate : FramedOrdinate
    {
        protected override string Offset { get; }

        public FramedToLeftOrdinate(Graph2D<Vertex> graph)
            : base(graph.Length)
        {
            Offset = string.Empty;
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
    }
}