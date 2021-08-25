using Common.ValueRanges;
using ConsoleVersion.Model;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using GraphLib.Realizations.Coordinates;
using GraphLib.Realizations.Graphs;
using System;
using static ConsoleVersion.Resource.Resources;

namespace ConsoleVersion.InputClass
{
    internal static class Input
    {
        /// <summary>
        /// Return user's console input in range of values
        /// </summary>
        /// <param name="accompanyingMessage">An input message</param>
        /// <param name="upperRangeValue">An upper value of input range</param>
        /// <param name="lowerRangeValue">A lower value of input range</param>
        /// <returns>A number in the range from 
        /// <paramref name="lowerRangeValue"/> to 
        /// <paramref name="upperRangeValue"/></returns>
        /// <exception cref="System.IO.IOException"></exception>
        public static int InputNumber(string accompanyingMessage,
            int upperRangeValue = int.MaxValue, int lowerRangeValue = 0)
        {
            var rangeOfValidInput = new InclusiveValueRange<int>(upperRangeValue, lowerRangeValue);
            return InputNumber(accompanyingMessage, rangeOfValidInput);
        }

        /// <summary>
        /// Return user's console input in range of values
        /// </summary>
        /// <param name="accompanyingMessage"></param>
        /// <param name="rangeOfValidInput"></param>
        /// <returns>A number in the range
        /// <paramref name="rangeOfValidInput"/></returns>
        /// <exception cref="System.IO.IOException"></exception>
        public static int InputNumber(string accompanyingMessage,
            InclusiveValueRange<int> rangeOfValidInput)
        {
            string userInput;
            do
            {
                Console.Write(accompanyingMessage);
                userInput = Console.ReadLine();
            } while (!IsValidInput(userInput, rangeOfValidInput));

            return Convert.ToInt32(userInput);
        }

        /// <summary>
        /// Returns <see cref="Coordinate2D"/> where X belongs to 
        /// [<paramref name="upperPossibleXValue"/>, 0]
        /// and where Y belongs to [<paramref name="upperPossibleYValue"/>, 0]
        /// </summary>
        /// <param name="upperPossibleXValue">An upper value of X
        /// coordinate in range where a lower value is 0</param>
        /// <param name="upperPossibleYValue">An upper value of Y 
        /// coordinate in range where a lower value is 0</param>
        /// <returns>An instance of <see cref="Coordinate2D"/></returns>
        public static Coordinate2D InputPoint(int upperPossibleXValue, int upperPossibleYValue)
        {
            int xCoordinate = InputNumber(XCoordinateInputMsg, upperPossibleXValue);
            int yCoordinate = InputNumber(YCoordinateInputMsg, upperPossibleYValue);

            return new Coordinate2D(xCoordinate, yCoordinate);
        }

        public static InclusiveValueRange<int> InputRange(InclusiveValueRange<int> rangeOfValiInput)
        {
            int upperValueOfRange = InputNumber(UpperValueOfRangeMsg, rangeOfValiInput);
            int lowerValueOfRange = InputNumber(LowerValueOfRangeMsg, rangeOfValiInput);

            return new InclusiveValueRange<int>(upperValueOfRange, lowerValueOfRange);
        }

        public static IVertex InputVertex(Graph2D graph2D)
        {
            var upperPossibleXValue = graph2D.Width - 1;
            var upperPossibleYValue = graph2D.Length - 1;

            var point = InputPoint(upperPossibleXValue, upperPossibleYValue);

            return (graph2D[point] as Vertex) ?? (IVertex)new NullVertex();
        }

        private static bool IsValidInput(string userInput,
            InclusiveValueRange<int> rangeOfValidInput)
        {
            return int.TryParse(userInput, out var input)
                && rangeOfValidInput.Contains(input);
        }
    }
}
