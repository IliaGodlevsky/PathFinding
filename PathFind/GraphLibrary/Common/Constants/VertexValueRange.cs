using System;

namespace GraphLibrary.Common.Constants
{
    public static class VertexValueRange
    {
        private static readonly Random rand;
        private static readonly int upperValue;
        private static readonly int lowerValue;

        static VertexValueRange()
        {
            rand = new Random();
            upperValue = 9;
            lowerValue = 1;
        }

        public static int ReturnValueInValueRange(int value)
        {
            if (value > upperValue)
                value = lowerValue;
            else if (value < lowerValue)
                value = upperValue;
            return value;
        }

        public static int GetRandomVertexValue()
        {
            return rand.Next(upperValue) + lowerValue;
        }
    }
}