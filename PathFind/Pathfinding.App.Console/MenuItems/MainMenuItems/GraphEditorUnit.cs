using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Units;
using System.Collections.Generic;

namespace Pathfinding.App.Console.MenuItems.MainMenuItems
{
    internal sealed class GraphEditorUnit : Unit
    {
        public GraphEditorUnit(IReadOnlyCollection<IMenuItem> menuItems,
            IReadOnlyCollection<IConditionedMenuItem> conditioned)
            : base(menuItems, conditioned)
        {
        }
    }
}
