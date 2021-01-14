using Common.ValueRanges;
using GraphLib.Coordinates;
using System;

namespace ConsoleVersion.InputClass
{
    internal static class Input
    {
        /// <summary>
        /// Return user's console input in range of values
        /// </summary>
        /// <param name="msg">An input message</param>
        /// <param name="upper">An upper value of input range</param>
        /// <param name="lower">A lower value of input range</param>
        /// <returns>A number in the range from 
        /// <paramref name="lower"/> to <paramref name="upper"/></returns>
        public static int InputNumber(string msg, int upper, int lower = 0)
        {
            var range = new ValueRange(upper, lower);
            string choice;
            do
            {
                Console.Write(msg);
                choice = Console.ReadLine();
            } while (IsError(choice, range));

            return Convert.ToInt32(choice);
        }

        /// <summary>
        /// Returns <see cref="Coordinate2D"/> where X belongs to [<paramref name="upperPossibleXValue"/>, 0]
        /// and where Y belongs to [<paramref name="upperPossibleYValue"/>, 0]
        /// </summary>
        /// <param name="upperPossibleXValue">An upper value of X coordinate in range where a lower value is 0</param>
        /// <param name="upperPossibleYValue">An upper value of Y coordinate in range where a lower value is 0</param>
        /// <returns>An instance of <see cref="Coordinate2D"/></returns>
        public static Coordinate2D InputPoint(int upperPossibleXValue, int upperPossibleYValue)
        {
            int xCoordinate = InputNumber(ConsoleVersionResources.XCoordinateInputMsg, upperPossibleXValue);
            int yCoordinate = InputNumber(ConsoleVersionResources.YCoordinateInputMsg, upperPossibleYValue);

            return new Coordinate2D(xCoordinate, yCoordinate);
        }

        private static bool IsError(string choice, ValueRange range)
        {
            return !int.TryParse(choice, out var ch) 
                || !range.IsInBounds(ch);
        }
    }
}
