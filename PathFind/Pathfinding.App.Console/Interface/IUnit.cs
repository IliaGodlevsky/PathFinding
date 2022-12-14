using System.Collections.Generic;

namespace Pathfinding.App.Console.Interface
{
    internal interface IUnit
    {
        IReadOnlyCollection<IMenuItem> MenuItems { get; }
    }
}
