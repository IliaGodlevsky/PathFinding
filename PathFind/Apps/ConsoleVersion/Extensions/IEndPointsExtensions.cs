using ConsoleVersion.Model;
using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;
using System;
using System.Collections.Generic;

namespace ConsoleVersion.Extensions
{
    internal static class IEndPointsExtensions
    {
        public static void ChooseEndPoints(this IEndPoints endPoints, Graph2D graph, IEnumerable<string> messages,
            Func<Graph2D, IEndPoints, string, Vertex> func, Action<Vertex> markAction)
        {
            foreach (var message in messages)
            {
                var vertex = func.Invoke(graph, endPoints, message);
                markAction.Invoke(vertex);
            }
        }
    }
}
