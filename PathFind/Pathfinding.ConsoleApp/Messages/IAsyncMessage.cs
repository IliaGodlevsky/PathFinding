using System;

namespace Pathfinding.ConsoleApp.Messages
{
    internal interface IAsyncMessage<T>
    {
        Action<T> Signal { get; set; }
    }
}
