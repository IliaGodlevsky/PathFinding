using System.Collections.Generic;

namespace ConsoleVersion.Interface
{
    internal interface IMenu
    {
        IReadOnlyList<IMenuCommand> Commands { get; }
    }
}
