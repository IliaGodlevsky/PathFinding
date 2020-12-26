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

        public static Coordinate2D InputPoint(int width, int height)
        {
            int xCoordinate = InputNumber(ConsoleVersionResources.XCoordinateInputMsg, width);
            int yCoordinate = InputNumber(ConsoleVersionResources.YCoordinateInputMsg, height);

            return new Coordinate2D(xCoordinate, yCoordinate);
        }

        private static bool IsError(string choice, ValueRange range)
        {
            return !int.TryParse(choice, out var ch) 
                || !range.IsInBounds(ch);
        }

        private static readonly int minMenuValue;
        private static readonly int maxMenuValue;
    }
}
