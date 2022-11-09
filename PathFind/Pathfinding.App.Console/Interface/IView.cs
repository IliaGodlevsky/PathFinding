using System;

namespace Pathfinding.App.Console.Interface
{
    internal interface IView : IDisplayable
    {
        event Action IterationStarted;
    }
}
