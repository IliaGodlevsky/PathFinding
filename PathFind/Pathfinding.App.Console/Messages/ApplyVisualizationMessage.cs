namespace Pathfinding.App.Console.Messages
{
    internal sealed class ApplyVisualizationMessage
    {
        public bool IsApplied { get; }

        public ApplyVisualizationMessage(bool isApplied)
        {
            IsApplied = isApplied;
        }
    }
}
