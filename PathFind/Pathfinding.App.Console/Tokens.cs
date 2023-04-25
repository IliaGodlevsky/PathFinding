using System;

namespace Pathfinding.App.Console
{
    [Flags]
    internal enum Tokens : long
    {
        Screen = 2 << 0,
        Main = 2 << 1,
        History = 2 << 2,
        Statistics = 2 << 3,
        Visualization = 2 << 4,
        Graph = 2 << 5,
        Pathfinding = 2 << 6,
        Path = 2 << 7,
        Range = 2 << 8,
        Common = 2 << 9
    }
}