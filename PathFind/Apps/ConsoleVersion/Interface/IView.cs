using System;

namespace ConsoleVersion.Interface
{
    internal interface IView : IDisplayable
    {
        event Action NewMenuIteration;
    }
}
