using System;

namespace Pathfinding.ConsoleApp.Messages
{
    internal interface IMayBeAsync
    {
        Action Signal { get; set; }
    }
}
