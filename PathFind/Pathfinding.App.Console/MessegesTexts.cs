using Pathfinding.App.Console.Model;

namespace Pathfinding.App.Console
{
    internal static class MessagesTexts
    {
        private static readonly string AnswerOptions = $"<{string.Join("/", Answer.Answers)}>";

        public static readonly string ApplyVisualizationMsg = $"Apply visualization {AnswerOptions}?: ";
        public static readonly string ExitAppMsg = $"Do you want to exit {AnswerOptions}?: ";
        public static readonly string ApplyHistoryMsg = $"Apply history recording {AnswerOptions}?: ";
        public static readonly string ApplyStatisticsMsg = $"Apply statistics {AnswerOptions}?: ";

        public const string PathfindingStatisticsFormat = "Steps: {0}  Path cost: {1}  Visited: {2}";
        public const string InProcessStatisticsFormat = "Visited: {0}";
        public const string AlgorithmChoiceMsg = "Choose algorithm: ";
        public const string GraphAssembleChoiceMsg = "Choose graph assemble: ";
        public const string DelayTimeInputMsg = "Input animation speed delay time (milliseconds): ";
        public const string GraphHeightInputMsg = "Input height of graph: ";
        public const string GraphWidthInputMsg = "Input width of graph: ";
        public const string RangeLowerValueInputMsg = "Input lower value of range: ";
        public const string RangeUpperValueInputMsg = "Input upper value of range: ";
        public const string ObstaclePercentInputMsg = "Input percent of obstacles: ";
        public const string MenuOptionChoiceMsg = "Choose menu option: ";
        public const string OutOfRangeMsg = "Value is out of range. Try again: ";
        public const string BadInputMsg = "Bad value input. Try again: ";
        public const string InputPathMsg = "Input path: ";
    }
}