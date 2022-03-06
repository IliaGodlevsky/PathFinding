using ConsoleVersion.Interface;
using ConsoleVersion.Model;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using GraphLib.Realizations.Graphs;
using System;
using System.Collections.Generic;
using ValueRange;

namespace ConsoleVersion.Extensions
{
    internal static class IValueInputExtensions
    {
        public static T InputValue<T>(this IValueInput<T> self, string msg, InclusiveValueRange<T> range)
            where T : struct, IComparable
        {
            return self.InputValue(msg, range.UpperValueOfRange, range.LowerValueOfRange);
        }

        public static InclusiveValueRange<T> InputRange<T>(this IValueInput<T> self, InclusiveValueRange<T> range)
            where T : struct, IComparable
        {
            T upperValueOfRange = self.InputValue(MessagesTexts.RangeUpperValueInputMsg, range);
            T lowerValueOfRange = self.InputValue(MessagesTexts.RangeLowerValueInputMsg, range);

            return new InclusiveValueRange<T>(upperValueOfRange, lowerValueOfRange);
        }

        public static Vertex InputVertex(this IValueInput<int> self, Graph2D graph2D)
        {
            int upperPossibleXValue = graph2D.Width - 1;
            int upperPossibleYValue = graph2D.Length - 1;

            var point = self.InputCoordinate(upperPossibleXValue, upperPossibleYValue);

            return (Vertex)graph2D[point];
        }

        public static Vertex InputEndPoint(this IValueInput<int> self, Graph2D graph, IEndPoints endPoints)
        {
            return self.InputVertex(graph, endPoints.CanBeEndPoint);
        }

        public static IEnumerable<Vertex> InputExistingIntermediates(this IValueInput<int> self, Graph2D graph, IEndPoints endPoints, int count)
        {
            return self.InputVertices(graph, endPoints.IsIntermediate, count);
        }

        public static IEnumerable<Vertex> InputRequiredEndPoints(this IValueInput<int> self, Graph2D graph, IEndPoints endPoints)
        {
            return self.InputEndPoints(graph, endPoints, 2);
        }

        public static IEnumerable<Vertex> InputEndPoints(this IValueInput<int> self, Graph2D graph, IEndPoints endPoints, int count)
        {
            return self.InputVertices(graph, endPoints.CanBeEndPoint, count);
        }

        private static IEnumerable<Vertex> InputVertices(this IValueInput<int> self, Graph2D graph, Predicate<IVertex> predicate, int count)
        {
            while (count-- > 0)
            {
                yield return self.InputVertex(graph, predicate);
            }
        }

        private static Vertex InputVertex(this IValueInput<int> self, Graph2D graph, Predicate<IVertex> predicate)
        {
            Vertex vertex;
            do
            {
                vertex = self.InputVertex(graph);
            }
            while (!predicate(vertex));

            return vertex;
        }

        private static Coordinate2D InputCoordinate(this IValueInput<int> self, int upperPossibleXValue, int upperPossibleYValue)
        {
            int xCoordinate = self.InputValue(MessagesTexts.XCoordinateInputMsg, upperPossibleXValue);
            int yCoordinate = self.InputValue(MessagesTexts.YCoordinateInputMsg, upperPossibleYValue);

            return new Coordinate2D(xCoordinate, yCoordinate);
        }
    }
}