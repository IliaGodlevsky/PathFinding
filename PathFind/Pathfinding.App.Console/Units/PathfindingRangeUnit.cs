using Pathfinding.App.Console.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.Console.ViewModel
{
    internal sealed class PathfindingRangeUnit : Unit
    {
        public PathfindingRangeUnit(IReadOnlyCollection<IMenuItem> menuItems) 
            : base(menuItems)
        {

        }
    }
}