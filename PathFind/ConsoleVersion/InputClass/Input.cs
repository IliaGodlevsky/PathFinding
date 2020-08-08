using System;
using System.Drawing;
using System.Linq;
using static ConsoleVersion.Forms.ConsoleMenu;

namespace ConsoleVersion.InputClass
{
    public static class Input
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
            var options = Enum.GetValues(typeof(MenuOption));
            return (MenuOption)InputNumber(Res.OptionMsg, options.Cast<int>().Last());
        }

        public static Point InputPoint(int width, int height) => 
            new Point(InputNumber(Res.XCoordinateMsg, width),
                InputNumber(Res.YCoordinateMsg, height));

        private static bool IsError(string choice, int upper, int lower)
        {
            return !int.TryParse(choice, out int ch) 
                || ch > upper || ch < lower;
        }
    }
}
