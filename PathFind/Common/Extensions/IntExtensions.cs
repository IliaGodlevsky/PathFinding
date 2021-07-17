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

        public static long Pow(this int value, int power)
        {
            return power < 0 ? default : (long)Enumerable.Repeat(value, power).Aggregate(1, Multiply);
        }

        public static int Multiply(this int value, int value2)
        {
            return value * value2;
        }

        public static int GetFlooredLog10(this int value)
        {
            if (value == 0)
                value = 1;
            return Convert.ToInt32(Math.Floor(Math.Log10(value)));
        }
    }
}
