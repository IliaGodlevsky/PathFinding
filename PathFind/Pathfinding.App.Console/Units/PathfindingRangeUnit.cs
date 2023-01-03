using Pathfinding.App.Console.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Units
{
    internal sealed class PathfindingRangeUnit : Unit
    {
        public PathfindingRangeUnit(IReadOnlyCollection<IMenuItem> menuItems, 
            IReadOnlyCollection<IConditionedMenuItem> conditioned)
            : base(menuItems, conditioned)
        {

        }
    }
}