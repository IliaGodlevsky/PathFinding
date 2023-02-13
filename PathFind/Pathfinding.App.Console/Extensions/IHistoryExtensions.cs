using Pathfinding.AlgorithmLib.History.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Extensions
{
    internal static class IHistoryExtensions
    {
        public static void AddRange(this IHistory<ConsoleColor> history, Guid key, IEnumerable<Vertex> vertices)
        {
            foreach (var vertex in vertices)
            {
                history.Add(key, vertex.Position, vertex.Color);
            }
        }

        public static void Add(this IHistory<ConsoleColor> history, Guid key, Vertex vertex)
        {
            history.Add(key, vertex.Position, vertex.Color);
        }
    }
}
