namespace GraphLibrary.Extensions.SystemTypeExtensions
{
    public static class IntExtensions
    {
        public static bool IsEven(this int number) => number % 2 == 0;
        public static bool IsOdd(this int number) => !number.IsEven();
    }
}
