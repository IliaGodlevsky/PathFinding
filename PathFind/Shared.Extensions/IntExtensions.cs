using System;

namespace Shared.Extensions
{
    public static class IntExtensions
    {
        public static int GetPercentage(this int value, double percent)
        {
            return (int)Math.Round(value * percent / 100.0, 0);
        }
    }
}
