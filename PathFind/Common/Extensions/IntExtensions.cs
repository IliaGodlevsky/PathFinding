using System;
using System.Linq;

namespace Common.Extensions
{
    public static class IntExtensions
    {
        public static int Xor(this int value, int value2)
        {
            return value ^ value2;
        }

        public static ulong Pow(this int value, int power)
        {
            return power < 0 ? default : (ulong)Enumerable.Repeat(value, power).Aggregate(1, Multiply);
        }

        public static int Multiply(this int value, int value2)
        {
            return value * value2;
        }

        public static int GetPercentage(this int value, double percent)
        {
            return (int)Math.Round(value * percent / 100.0, 0);
        }
    }
}
