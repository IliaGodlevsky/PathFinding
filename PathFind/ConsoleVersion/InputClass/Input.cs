using ConsoleVersion.Enums;
using GraphLibrary.Coordinates;
using GraphLibrary.Vertex.Interface;
using System;
using System.Drawing;
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

        public static int InputNumber(string msg, int upper, int lower = 0)
        {
            string choice;
            Console.Write(msg);
            choice = Console.ReadLine();
            while (IsError(choice, upper, lower)) 
            {
                Console.Write(msg);
                choice = Console.ReadLine();
            }
            return int.Parse(choice);
        }

        public static MenuOption InputOption()
        {
            return (MenuOption)InputNumber(
                ConsoleVersionResources.OptionMsg,
                maxMenuValue, minMenuValue);
        }

        public static Coordinate2D InputPoint(int width, int height)
        {
            return new Coordinate2D(InputNumber(ConsoleVersionResources.XCoordinateMsg, width),
                                InputNumber(ConsoleVersionResources.YCoordinateMsg, height));
        }

        private static bool IsError(string choice, int upper, int lower)
        {
            return !int.TryParse(choice, out var ch) 
                || ch > upper || ch < lower;
        }

        private static readonly byte minMenuValue;
        private static readonly byte maxMenuValue;
    }
}
