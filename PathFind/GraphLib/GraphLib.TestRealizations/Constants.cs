namespace GraphLib.TestRealizations
{
    internal static class Constants
    {
        static Constants()
        {
            TestGraph2DDimensionSizes = new[] { Width, Length };
        }

        private const int Width = 80;
        private const int Length = 45;

        public static int[] TestGraph2DDimensionSizes { get; }
    }
}
