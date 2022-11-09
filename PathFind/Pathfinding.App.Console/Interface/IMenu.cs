using System.Collections.Generic;

namespace Pathfinding.App.Console.Interface
{
    internal interface IMenu
    {
        IReadOnlyList<IMenuCommand> Commands { get; }
    }
}
