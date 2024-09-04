namespace Pathfinding.ConsoleApp.Messages.ViewModel
{
    internal sealed class ObstaclesCountChangedMessage
    {
        public int GraphId { get; set; }

        public int Delta { get; set; }
    }
}
