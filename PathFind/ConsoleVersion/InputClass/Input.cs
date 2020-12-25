using Common.ValueRanges;
using ConsoleVersion.Enums;
using GraphLib.Coordinates;
using System;
using System.Linq;

namespace ConsoleVersion.InputClass
{
    internal static class Input
    {
        static Input()
        {
            var menuValues = Enum.GetValues(typeof(MenuOption)).Cast<byte>();
            minMenuValue = menuValues.First();
            maxMenuValue = menuValues.Last();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg">An input message</param>
        /// <param name="upper">An upper value of input range</param>
        /// <param name="lower">A lower value of input range</param>
        /// <returns></returns>
        public static int InputNumber(string msg, int upper, int lower = 0)
        {
            var range = new ValueRange(upper, lower);
            Console.Write(msg);
            string choice = Console.ReadLine();
            while (IsError(choice, range))
            {
                Console.Write(msg);
                choice = Console.ReadLine();
            }
            return Convert.ToInt32(choice);
        }

        public static MenuOption InputOption()
        {
            var format = ConsoleVersionResources.OptionInputMsg;
            int option = InputNumber(format, maxMenuValue, minMenuValue);
            return (MenuOption)option;
        }

        public static Coordinate2D InputPoint(int width, int height)
        {
            int xCoordinate = InputNumber(ConsoleVersionResources.XCoordinateInputMsg, width);
            int yCoordinate = InputNumber(ConsoleVersionResources.YCoordinateInputMsg, height);

            return new Coordinate2D(xCoordinate, yCoordinate);
        }

        private static bool IsError(string choice, ValueRange range)
        {
            return !int.TryParse(choice, out var ch) || !range.IsInBounds(ch);
        }

        private static readonly byte minMenuValue;
        private static readonly byte maxMenuValue;
    }
}
