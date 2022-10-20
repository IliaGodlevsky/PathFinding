using System;

namespace Common.Extensions
{
    public static class TupleExtensions
    {
        public static Enumerator GetEnumerator(this (int LowerValueOfRange, int UpperValueOfRange) range)
        {
            if (range.LowerValueOfRange > range.UpperValueOfRange)
            {
                throw new ArgumentOutOfRangeException();
            }
            return new Enumerator(range.LowerValueOfRange, range.UpperValueOfRange);
        }
    }   
}
