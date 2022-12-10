using Autofac.Features.AttributeFilters;
using Pathfinding.App.Console.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.Console.ViewModel
{
    internal sealed class PathfindingRangeUnit : Unit
    {
        public PathfindingRangeUnit([KeyFilter(typeof(PathfindingRangeUnit))]IReadOnlyCollection<IMenuItem> menuItems) 
            : base(menuItems)
        {

        }
    }
}