using System;

namespace WPFVersion3D
{
    internal static class MessageTokens
    {
        public static Guid MainModel { get; }
        public static Guid AlgorithmStatisticsModel { get; }
        public static Guid PathfindingModel { get; }

        static MessageTokens()
        {
            MainModel = Guid.NewGuid();
            AlgorithmStatisticsModel = Guid.NewGuid();
            PathfindingModel = Guid.NewGuid();
        }
    }
}