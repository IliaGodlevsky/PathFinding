using System.Collections.Generic;

namespace ConsoleVersion.Interface
{
    internal interface IMenu
    {
        IReadOnlyDictionary<string, IMenuCommand> MenuCommands { get; }
    }
}
