namespace Pathfinding.App.Console.Messages
{
    internal sealed class ApplyStatisticsMessage
    {
        public bool IsApplied { get; }

        public ApplyStatisticsMessage(bool isApplied)
        {
            IsApplied = isApplied;
        }
    }
}
