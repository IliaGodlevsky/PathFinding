namespace Pathfinding.App.Console.Messages
{
    internal sealed class IsAppliedMessage
    {
        public bool IsApplied { get; }

        public IsAppliedMessage(bool isApplied)
        {
            IsApplied = isApplied;
        }
    }
}
