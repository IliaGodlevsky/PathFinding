using ConsoleVersion.Enums;
using System;
using System.Drawing;
using System.Linq;

namespace ConsoleVersion.InputClass
{
    internal static class Input
    {
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
            return (MenuOption)InputNumber(ConsoleVersionResources.OptionMsg, 
                Enum.GetValues(typeof(MenuOption)).Cast<byte>().Last());
        }

        public static Point InputPoint(int width, int height) => 
            new Point(InputNumber(ConsoleVersionResources.XCoordinateMsg, width),
                InputNumber(ConsoleVersionResources.YCoordinateMsg, height));

        private static bool IsError(string choice, int upper, int lower)
        {
            return !int.TryParse(choice, out var ch) 
                || ch > upper || ch < lower;
        }
    }
}
