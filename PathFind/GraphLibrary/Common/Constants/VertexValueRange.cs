using System;

namespace GraphLibrary.Common.Constants
{
    public static class VertexValueRange
    {
        private static readonly Random rand;
        public static int UpperValue { get; }
        public static int LowerValue { get; }

        static VertexValueRange()
        {
            rand = new Random();
            UpperValue = 9;
            LowerValue = 1;
        }

        public static int ReturnValueInValueRange(int value)
        {
            if (value > UpperValue)
                value = LowerValue;
            else if (value < LowerValue)
                value = UpperValue;
            return value;
        }

        public static int GetRandomVertexValue()
        {
            return rand.Next(UpperValue) + LowerValue;
        }
    }
}