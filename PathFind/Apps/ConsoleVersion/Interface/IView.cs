using System;

namespace ConsoleVersion.Interface
{
    internal interface IView
    {
        event Action NewMenuIteration;

        void Start();
    }
}
