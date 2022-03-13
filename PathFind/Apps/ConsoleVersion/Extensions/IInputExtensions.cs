using ConsoleVersion.Enums;
using ConsoleVersion.Interface;
using ConsoleVersion.Model;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using GraphLib.Realizations.Graphs;
using System;
using System.Collections.Generic;
using ValueRange;
using ValueRange.Extensions;

namespace ConsoleVersion.Extensions
{
    internal static class IInputExtensions
    {
        public static T Input<T>(this IInput<T> self, string msg)
            where T : IComparable
        {
            Console.Write(msg);
            return self.Input();
        }

        public static T Input<T>(this IInput<T> self, string msg, T upper, T lower = default)
            where T : IComparable
        {
            var range = (upper, lower).ToRange();
            var input = self.Input(msg);
            while (!range.Contains(input))
            {
                Console.Write(MessagesTexts.OutOfRangeMsg);
                input = self.Input(msg);
            }
            return input;
        }

        public static T Input<T>(this IInput<T> self, string msg, InclusiveValueRange<T> range)
            where T : IComparable
        {
            return self.Input(msg, range.UpperValueOfRange, range.LowerValueOfRange);
        }

        public static bool InputAnswer(this IInput<Answer> self, string msg, InclusiveValueRange<Answer> range)
        {
            return self.Input(msg, range) == Answer.Yes;
        }

        public static InclusiveValueRange<T> InputRange<T>(this IInput<T> self, InclusiveValueRange<T> range)
            where T : IComparable
        {
            T upperValueOfRange = self.Input(MessagesTexts.RangeUpperValueInputMsg, range);
            T lowerValueOfRange = self.Input(MessagesTexts.RangeLowerValueInputMsg, range);

            return new InclusiveValueRange<T>(upperValueOfRange, lowerValueOfRange);
        }

        public static Vertex InputVertex(this IInput<int> self, Graph2D graph2D)
        {
            int upperPossibleXValue = graph2D.Width - 1;
            int upperPossibleYValue = graph2D.Length - 1;

            var point = self.InputCoordinate(upperPossibleXValue, upperPossibleYValue);

            return (Vertex)graph2D[point];
        }

        public static Vertex InputEndPoint(this IInput<int> self, Graph2D graph, IEndPoints endPoints)
        {
            return self.InputVertex(graph, endPoints.CanBeEndPoint);
        }

        public static IEnumerable<Vertex> InputExistingIntermediates(this IInput<int> self, Graph2D graph, IEndPoints endPoints, int count)
        {
            return self.InputVertices(graph, endPoints.IsIntermediate, count);
        }

        public static IEnumerable<Vertex> InputRequiredEndPoints(this IInput<int> self, Graph2D graph, IEndPoints endPoints)
        {
            return self.InputEndPoints(graph, endPoints, 2);
        }

        public static IEnumerable<Vertex> InputEndPoints(this IInput<int> self, Graph2D graph, IEndPoints endPoints, int count)
        {
            return self.InputVertices(graph, endPoints.CanBeEndPoint, count);
        }

        private static IEnumerable<Vertex> InputVertices(this IInput<int> self, Graph2D graph, Predicate<IVertex> predicate, int count)
        {
            while (count-- > 0)
            {
                yield return self.InputVertex(graph, predicate);
            }
        }

        private static Vertex InputVertex(this IInput<int> self, Graph2D graph, Predicate<IVertex> predicate)
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