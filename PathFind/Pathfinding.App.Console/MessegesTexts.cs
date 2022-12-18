using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Model;

namespace Pathfinding.App.Console
{
    internal static class MessagesTexts
    {
        private static string AnswerOptions = $"<{string.Join("/", Answer.Answers)}>";

        public static string ApplyVisualizationMsg = string.Format(Languages.ApplyVisualizationMsg, AnswerOptions);
        public static string ExitAppMsg = string.Format(Languages.ExitMsg, AnswerOptions);
        public static string ApplyHistoryMsg = string.Format(Languages.ApplyHistoryMsg, AnswerOptions);
        public static string ApplyStatisticsMsg = string.Format(Languages.ApplyStatisticsMsg, AnswerOptions);
    }
}