using System;

namespace WPFVersion
{
    internal static class MessageTokens
    {
        public static Guid MainModel { get; }
        public static Guid AlgorithmStatisticsModel { get; }
        public static Guid PathfindingModel { get; }
        public static Guid VisualizationModel { get; }

        static MessageTokens()
        {
            MainModel = Guid.NewGuid();
            AlgorithmStatisticsModel = Guid.NewGuid();
            PathfindingModel = Guid.NewGuid();
            VisualizationModel = Guid.NewGuid();
        }
    }
}