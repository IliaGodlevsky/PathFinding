using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.MenuItems.MenuItemPriority;
using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Units;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Logging.Interface;
using Pathfinding.Service.Interface.Commands;

namespace Pathfinding.App.Console.MenuItems.PathfindingProcessMenuItems
{
    [HighestPriority]
    internal sealed class AlgorithmsUnitMenuItem(IInput<int> intInput,
        AlgorithmChooseUnit unit,
        IPathfindingRangeBuilder<Vertex> builder,
        ILog log) : UnitDisplayMenuItem<AlgorithmChooseUnit>(intInput, unit, log), IConditionedMenuItem
    {
        private readonly IPathfindingRangeBuilder<Vertex> builder = builder;

        public bool CanBeExecuted()
        {
            return builder.Range.HasSourceAndTargetSet()
                && !builder.Range.HasIsolators();
        }

        public override string ToString()
        {
            return Languages.FindPath;
        }
    }
}
