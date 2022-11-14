using System.Collections.Generic;

namespace Pathfinding.App.Console.Menu.Interface
{
    internal interface IMenuCommands
    {
        IReadOnlyList<IMenuCommand> Commands { get; }
    }
}
