using Common.ValueRanges;

namespace ConsoleVersion
{
    internal static class Constants
    {
        public const string MakeUnwieghted = "Make unweighted";
        public const string MakeWeighted = "Make weighted";
        public const string CreateNewGraph = "Create new graph";
        public const string FindPath = "Find path";
        public const string ReverseVertex = "Reverse vertex";
        public const string ChangeCostRange = "Change cost range";
        public const string ChangeVertexCost = "Change vertex cost";
        public const string SaveGraph = "Save graph";
        public const string LoadGraph = "Load graph";
        public const string QuitProgramm = "Quit program";

        public static InclusiveValueRange<int> GraphWidthValueRange { get; }
        public static InclusiveValueRange<int> GraphLengthValueRange { get; }
        public static InclusiveValueRange<int> ObstaclesPercentValueRange { get; }
        public static InclusiveValueRange<int> AlgorithmDelayTimeValueRange { get; }
        public static InclusiveValueRange<int> CostRange { get; }

        static Constants()
        {
            CostRange = new InclusiveValueRange<int>(99, 1);
            GraphWidthValueRange = new InclusiveValueRange<int>(80, 1);
            GraphLengthValueRange = new InclusiveValueRange<int>(50, 1);
            ObstaclesPercentValueRange = new InclusiveValueRange<int>(99, 0);
            AlgorithmDelayTimeValueRange = new InclusiveValueRange<int>(35, 0);
        }
    }
}
