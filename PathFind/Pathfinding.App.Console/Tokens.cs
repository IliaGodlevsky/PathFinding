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
        Common = 2 << 8,
        Regular = 2 << 9,
        Obstacle = 2 << 10,
        Source = 2 << 11,
        Target = 2 << 12,
        Transit = 2 << 13,
        Visited = 2 << 14,
        Enqueued = 2 << 15,
        Crossed = 2 << 16,
    }
}