﻿using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Core.Realizations.Coordinates;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using System;
using System.Collections.Generic;

using ColorfulConsole = Colorful.Console;

namespace Pathfinding.App.Console.Extensions
{
    internal static class IInputExtensions
    {
        public static T Input<T>(this IInput<T> self, string msg)
        {
            ColorfulConsole.Write(msg);
            return self.Input();
        }

        public static T Input<T>(this IInput<T> self, string msg, T upper, T lower = default)
            where T : IComparable, IComparable<T>
        {
            return self.Input(msg, new InclusiveValueRange<T>(upper, lower));
        }

        public static T Input<T>(this IInput<T> self, string msg, InclusiveValueRange<T> range)
            where T : IComparable, IComparable<T>
        {
            var input = self.Input(msg);
            while (!range.Contains(input))
            {
                ColorfulConsole.Write(MessagesTexts.OutOfRangeMsg);
                input = self.Input(msg);
            }
            return input;
        }

        public static InclusiveValueRange<T> InputRange<T>(this IInput<T> self, InclusiveValueRange<T> range)
            where T : IComparable, IComparable<T>
        {
            T upperValueOfRange = self.Input(MessagesTexts.RangeUpperValueInputMsg, range);
            T lowerValueOfRange = self.Input(MessagesTexts.RangeLowerValueInputMsg, range);

            return new InclusiveValueRange<T>(upperValueOfRange, lowerValueOfRange);
        }

        public static Vertex InputVertex(this IInput<int> self, Graph2D<Vertex> graph2D)
        {
            int upperPossibleXValue = graph2D.Width - 1;
            int upperPossibleYValue = graph2D.Length - 1;

            var point = self.InputCoordinate(upperPossibleXValue, upperPossibleYValue);

            return graph2D.Get(point);
        }

        public static Vertex InputVertex(this IInput<int> self, Graph2D<Vertex> graph, IPathfindingRange range)
        {
            return self.InputVertex(graph, range.CanBeInRange);
        }

        public static Vertex InputVertex(this IInput<int> self, string message, 
            Graph2D<Vertex> graph, IPathfindingRange range)
        {
            ColorfulConsole.WriteLine(message);
            return self.InputVertex(graph, range);
        }

        public static IEnumerable<Vertex> InputExistingIntermediates(this IInput<int> self, 
            Graph2D<Vertex> graph, IPathfindingRange range, int count)
        {
            return self.InputVertices(graph, range.IsIntermediate, count);
        }

        public static IEnumerable<Vertex> InputRequiredVertices(this IInput<int> self, 
            Graph2D<Vertex> graph, IPathfindingRange range)
        {
            return self.InputVertices(graph, range, 2);
        }

        public static IEnumerable<Vertex> InputVertices(this IInput<int> self, Graph2D<Vertex> graph, 
            IPathfindingRange range, int count)
        {
            return self.InputVertices(graph, range.CanBeInRange, count);
        }

        private static IEnumerable<Vertex> InputVertices(this IInput<int> self, Graph2D<Vertex> graph, 
            Predicate<IVertex> predicate, int count)
        {
            while (count-- > 0)
            {
                yield return self.InputVertex(graph, predicate);
            }
        }

        private static Vertex InputVertex(this IInput<int> self, Graph2D<Vertex> graph, Predicate<IVertex> predicate)
        {
            Vertex vertex;
            do
            {
                vertex = self.InputVertex(graph);
            }
            while (!predicate(vertex));

            return vertex;
        }

        private static Coordinate2D InputCoordinate(this IInput<int> self, int upperPossibleXValue, int upperPossibleYValue)
        {
            int xCoordinate = self.Input(MessagesTexts.XCoordinateInputMsg, upperPossibleXValue);
            int yCoordinate = self.Input(MessagesTexts.YCoordinateInputMsg, upperPossibleYValue);

            return new Coordinate2D(xCoordinate, yCoordinate);
        }
    }
}