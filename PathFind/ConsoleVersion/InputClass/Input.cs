using System;
using System.Drawing;

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

        public static Point InputPoint(int width, int height) => 
            new Point(InputNumber("Enter x coordinate of point: ", width),
                InputNumber("Enter y coordinate of point: ", height));

        private static bool IsError(string choice, int upper, int lower)
        {
            return !int.TryParse(choice, out int ch) 
                || ch > upper || ch < lower;
        }
    }
}
