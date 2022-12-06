using System;

namespace Pathfinding.App.Console.Interface
{
    internal interface IViewModel
    {
        //IReadOnlyCollection<IMenuItem> MenuItems { get; }

        event Action ViewClosed;
    }
}
