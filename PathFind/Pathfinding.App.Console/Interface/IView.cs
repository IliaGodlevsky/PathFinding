using System;

namespace Pathfinding.App.Console.Interface
{
    internal interface IView<TViewModel> : IDisplayable
        where TViewModel : IViewModel
    {
        event Action NewMenuCycleStarted;
    }
}
