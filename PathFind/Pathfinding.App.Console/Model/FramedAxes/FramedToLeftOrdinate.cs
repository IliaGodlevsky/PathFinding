using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;

namespace Pathfinding.App.Console.Model.FramedAxes
{
    internal sealed class FramedToLeftOrdinate : FramedOrdinate
    {
        protected override int FrameOffset { get; } = 0;

        protected override int ValueOffset { get; } = 0;

        public FramedToLeftOrdinate(IGraph<Vertex> graph)
            : base(graph.GetLength())
        {
            
        }

        protected override string GetPaddedIndex(int index)
        {
            return index.ToString().PadLeft(yCoordinatePadding);
        }
    }
}