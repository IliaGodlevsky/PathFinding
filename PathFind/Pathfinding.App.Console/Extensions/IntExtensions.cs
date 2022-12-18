namespace Pathfinding.App.Console.Extensions
{
    internal static class IntExtensions
    {
        public static int GetDigitsNumber(this int number)
        {
            int length = 1;
            if (number < 0)
            {
                length = 2;
                number = -number;
            }
            number /= 10;
            while (number > 0)
            {
                length++;
                number /= 10;
            }
            return length;
        }
    }
}
