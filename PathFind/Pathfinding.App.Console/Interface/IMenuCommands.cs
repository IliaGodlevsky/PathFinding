using System.Collections.Generic;

namespace Pathfinding.App.Console.Interface
{
    internal interface IMenuCommands
    {
        IReadOnlyList<IMenuCommand> Commands { get; }
    }
}
