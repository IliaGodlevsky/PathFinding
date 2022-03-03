using ConsoleVersion.Model;
using ConsoleVersion.ValueInput;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;
using System;
using System.Collections.Generic;

namespace ConsoleVersion.Extensions
{
    internal static class ConsoleValueInputExtensions
    {
        public static Vertex InputEndPoint(this ConsoleValueInput<int> self, Graph2D graph, IEndPoints endPoints)
        {
            return self.InputVertex(graph, endPoints.CanBeEndPoint);
        }

        public static IEnumerable<Vertex> InputExistingIntermediates(this ConsoleValueInput<int> self, Graph2D graph, IEndPoints endPoints, int count)
        {
            return self.InputVertices(graph, endPoints.IsIntermediate, count);
        }

        public static IEnumerable<Vertex> InputSourceAndTarget(this ConsoleValueInput<int> self, Graph2D graph, IEndPoints endPoints)
        {
            yield return self.InputEndPoint(graph, endPoints);
            yield return self.InputEndPoint(graph, endPoints);
        }

        public static IEnumerable<Vertex> InputIntermediates(this ConsoleValueInput<int> self, Graph2D graph, IEndPoints endPoints, int count)
        {
            return self.InputEndPoints(graph, endPoints, count);
        }

        private static IEnumerable<Vertex> InputEndPoints(this ConsoleValueInput<int> self,
            Graph2D graph, IEndPoints endPoints, int count)
        {
            return self.InputVertices(graph, endPoints.CanBeEndPoint, count);
        }

        private static IEnumerable<Vertex> InputVertices(this ConsoleValueInput<int> self,
            Graph2D graph, Predicate<IVertex> predicate, int count)
        {
            while (count-- > 0)
            {
                yield return self.InputVertex(graph, predicate);
            }
        }

        private static Vertex InputVertex(this ConsoleValueInput<int> self, Graph2D graph, Predicate<IVertex> predicate)
        {
            Vertex vertex;
            do
            {
                vertex = (Vertex)self.InputVertex(graph);
            }
            while (!predicate(vertex));
            return vertex;
        }
    }
}