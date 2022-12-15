using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Model;

namespace Pathfinding.App.Console
{
    internal static class MessagesTexts
    {
        private static readonly string AnswerOptions = $"<{string.Join("/", Answer.Answers)}>";

        public static readonly string ApplyVisualizationMsg = string.Format(Languages.ApplyVisualizationMsg, AnswerOptions);
        public static readonly string ExitAppMsg = string.Format(Languages.ExitMsg, AnswerOptions);
        public static readonly string ApplyHistoryMsg = string.Format(Languages.ApplyHistoryMsg, AnswerOptions);
        public static readonly string ApplyStatisticsMsg = string.Format(Languages.ApplyStatisticsMsg, AnswerOptions);

        public static readonly string PathfindingStatisticsFormat = Languages.PathfindingStatisticsFormat;
        public static readonly string InProcessStatisticsFormat = Languages.InProcessStatisticsFormat;
        public static readonly string AlgorithmChoiceMsg = Languages.AlgorithmChoiceMsg;
        public static readonly string GraphAssembleChoiceMsg = Languages.GraphAssembleChoiceMsg;
        public static readonly string DelayTimeInputMsg = Languages.DelayTimeInputMsg;
        public static readonly string GraphHeightInputMsg = Languages.GraphHeightInputMsg;
        public static readonly string GraphWidthInputMsg = Languages.GraphWidthInputMsg;
        public static readonly string RangeLowerValueInputMsg = Languages.RangeLowerValueInputMsg;
        public static readonly string RangeUpperValueInputMsg = Languages.RangeUpperValueInputMsg;
        public static readonly string ObstaclePercentInputMsg = Languages.ObstaclePercentInputMsg;
        public static readonly string MenuOptionChoiceMsg = Languages.MenuOptionChoiceMsg;
        public static readonly string OutOfRangeMsg = Languages.OutOfRangeMsg;
        public static readonly string BadInputMsg = Languages.BadInputMsg;
        public static readonly string InputPathMsg = Languages.InputPathMsg;
    }
}