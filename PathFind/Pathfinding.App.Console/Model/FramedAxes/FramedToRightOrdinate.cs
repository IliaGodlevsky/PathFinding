using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;

namespace Pathfinding.App.Console.Model.FramedAxes
{
    internal sealed class FramedToRightOrdinate : FramedOrdinate
    {
        private readonly int graphWidth;

        protected override int FrameOffset { get; }

        protected override int ValueOffset { get; }

        public FramedToRightOrdinate(IGraph<Vertex> graph)
            : base(graph.GetLength())
        {
            graphWidth = graph.GetWidth();
            FrameOffset = graphWidth * LateralDistance
                + AppLayout.WidthOfOrdinateView - yCoordinatePadding;
            ValueOffset = FrameOffset + AppLayout.WidthOfOrdinateView;
        }

        protected override string GetPaddedIndex(int index)
        {
            return index.ToString();
        }
    }
}