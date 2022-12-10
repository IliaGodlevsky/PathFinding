namespace Pathfinding.App.Console.Messages
{
    internal sealed class ApplyHistoryMessage
    {
        public bool IsApplied { get; }

        public ApplyHistoryMessage(bool isApplied)
        {
            IsApplied = isApplied;
        }
    }
}
