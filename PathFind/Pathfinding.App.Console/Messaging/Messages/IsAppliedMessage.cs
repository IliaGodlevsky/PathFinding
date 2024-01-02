namespace Pathfinding.App.Console.Messaging.Messages
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
