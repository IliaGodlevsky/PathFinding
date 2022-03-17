namespace GraphLib.TestRealizations
{
    internal static class Constants
    {
        static Constants()
        {
            DimensionSizes2D = new[] { Width, Length };
        }

        public const int Width = 25;
        public const int Length = 20;

        public static int[] DimensionSizes2D { get; }
    }
}
