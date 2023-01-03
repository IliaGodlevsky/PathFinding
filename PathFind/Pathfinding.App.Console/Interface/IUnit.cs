using System.Collections.Generic;

namespace Pathfinding.App.Console.Interface
{
    internal interface IUnit
    {
        IReadOnlyList<IMenuItem> GetMenuItems();
    }
}
