using Common.ValueRanges;
using ConsoleVersion.Enums;
using EnumerationValues.Extensions;
using EnumerationValues.Realizations;

namespace ConsoleVersion
{
    internal static class Constants
    {
        public static InclusiveValueRange<Answer> AnswerValueRange { get; }
        public static InclusiveValueRange<int> GraphWidthValueRange { get; }
        public static InclusiveValueRange<int> GraphLengthValueRange { get; }
        public static InclusiveValueRange<int> ObstaclesPercentValueRange { get; }
        public static InclusiveValueRange<int> AlgorithmDelayTimeValueRange { get; }
        public static InclusiveValueRange<int> VerticesCostRange { get; }

        static Constants()
        {
            AnswerValueRange = new EnumValues<Answer>().ToValueRange();
            VerticesCostRange = new InclusiveValueRange<int>(99, 1);
            GraphWidthValueRange = new InclusiveValueRange<int>(80, 1);
            GraphLengthValueRange = new InclusiveValueRange<int>(50, 1);
            ObstaclesPercentValueRange = new InclusiveValueRange<int>(99);
            AlgorithmDelayTimeValueRange = new InclusiveValueRange<int>(35);
        }
    }
}
