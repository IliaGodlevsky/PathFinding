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
    internal static class ValueInputExtensions
    {
        public static TValue InputValue<TValue>(this IValueInput<TValue> self, string accompanyingMessage, InclusiveValueRange<TValue> range)
            where TValue : struct, IComparable
        {
            return self.InputValue(accompanyingMessage, range.UpperValueOfRange, range.LowerValueOfRange);
        }

        public static InclusiveValueRange<TValue> InputRange<TValue>(this IValueInput<TValue> self, InclusiveValueRange<TValue> range)
            where TValue : struct, IComparable
        {
            TValue upperValueOfRange = self.InputValue(MessagesTexts.RangeUpperValueInputMsg, range);
            TValue lowerValueOfRange = self.InputValue(MessagesTexts.RangeLowerValueInputMsg, range);

            return new InclusiveValueRange<TValue>(upperValueOfRange, lowerValueOfRange);
        }

        public static IVertex InputVertex(this IValueInput<int> self, Graph2D graph2D)
        {
            int upperPossibleXValue = graph2D.Width - 1;
            int upperPossibleYValue = graph2D.Length - 1;

            var point = self.InputCoordinate(upperPossibleXValue, upperPossibleYValue);

            return graph2D[point];
        }

        public static Vertex InputEndPoint(this IValueInput<int> self, Graph2D graph, IEndPoints endPoints)
        {
            return self.InputVertex(graph, endPoints.CanBeEndPoint);
        }

        public static IEnumerable<Vertex> InputExistingIntermediates(this IValueInput<int> self, Graph2D graph, IEndPoints endPoints, int count)
        {
            return self.InputVertices(graph, endPoints.IsIntermediate, count);
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
                vertex = (Vertex)self.InputVertex(graph);
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
