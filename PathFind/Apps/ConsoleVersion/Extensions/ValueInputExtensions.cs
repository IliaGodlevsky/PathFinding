using ConsoleVersion.Interface;
using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using GraphLib.Realizations.Graphs;
using System;
using ValueRange;

namespace ConsoleVersion.Extensions
{
    internal static class ValueInputExtensions
    {
        public static TValue InputValue<TValue>(this IValueInput<TValue> self,
            string accompanyingMessage,
            InclusiveValueRange<TValue> rangeOfValidInput)
            where TValue : struct, IComparable
        {
            return self.InputValue(accompanyingMessage,
                rangeOfValidInput.UpperValueOfRange,
                rangeOfValidInput.LowerValueOfRange);
        }

        public static ICoordinate InputPoint(this IValueInput<int> self,
            int upperPossibleXValue, int upperPossibleYValue)
        {
            int xCoordinate = self.InputValue(MessagesTexts.XCoordinateInputMsg, upperPossibleXValue);
            int yCoordinate = self.InputValue(MessagesTexts.YCoordinateInputMsg, upperPossibleYValue);

            return new Coordinate2D(xCoordinate, yCoordinate);
        }

        public static InclusiveValueRange<TValue> InputRange<TValue>(this IValueInput<TValue> self,
            InclusiveValueRange<TValue> rangeOfValiInput)
            where TValue : struct, IComparable
        {
            TValue upperValueOfRange = self.InputValue(MessagesTexts.RangeUpperValueInputMsg, rangeOfValiInput);
            TValue lowerValueOfRange = self.InputValue(MessagesTexts.RangeLowerValueInputMsg, rangeOfValiInput);

            return new InclusiveValueRange<TValue>(upperValueOfRange, lowerValueOfRange);
        }

        public static IVertex InputVertex(this IValueInput<int> self, Graph2D graph2D)
        {
            int upperPossibleXValue = graph2D.Width - 1;
            int upperPossibleYValue = graph2D.Length - 1;

            var point = self.InputPoint(upperPossibleXValue, upperPossibleYValue);

            return graph2D[point];
        }
    }
}
