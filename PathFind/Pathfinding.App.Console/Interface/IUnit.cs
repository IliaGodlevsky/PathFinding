using System.Collections.Generic;

namespace Pathfinding.App.Console.Interface
{
    internal interface IUnit
    {
        int MenuItemColumns { get; }

        IReadOnlyCollection<IMenuItem> MenuItems { get; }
    }
}
