using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Modules.Interface;

namespace Pathfinding.App.Console.MenuItems.PathfindingRangeMenuItems
{
    internal sealed class ClearPathfindingRangeMenuItem : IMenuItem
    {
        private readonly IPathfindingRangeBuilder<Vertex> rangeBuilder;

        public int Order => 6;

        public ClearPathfindingRangeMenuItem(IPathfindingRangeBuilder<Vertex> rangeBuilder)
        {
            this.rangeBuilder = rangeBuilder;
        }

        public bool CanBeExecuted() => true;

        public void Execute()
        {
            rangeBuilder.Undo();
        }

        public override string ToString()
        {
            return "Clear pathfinding range";
        }
    }
}
