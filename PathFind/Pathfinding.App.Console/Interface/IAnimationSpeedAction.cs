using System;

namespace Pathfinding.App.Console.Interface
{
    internal interface IAnimationSpeedAction
    {
        TimeSpan Do(TimeSpan current);
    }
}
