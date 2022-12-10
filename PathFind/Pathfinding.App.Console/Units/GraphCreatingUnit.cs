using Autofac.Features.AttributeFilters;
using Pathfinding.App.Console.Interface;
using System.Collections.Generic;

namespace Pathfinding.App.Console.ViewModel
{
    internal sealed class GraphCreatingUnit : Unit
    {
        public GraphCreatingUnit([KeyFilter(typeof(GraphCreatingUnit))]IReadOnlyCollection<IMenuItem> menuItems)
            : base(menuItems)
        {

        }
    }
}