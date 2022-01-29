using System;

namespace Common.Extensions
{
    public static class IntExtensions
    {
        public static int Xor(this int value, int value2)
        {
            return value ^ value2;
        }

        public static int GetPercentage(this int value, double percent)
        {
            return (int)Math.Round(value * percent / 100.0, 0);
        }
    }
}
